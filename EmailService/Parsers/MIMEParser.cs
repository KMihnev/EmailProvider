using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace EmailService.Parsers
{
    public class MimePart
    {
        public Dictionary<string, string> Headers { get; set; } = new(StringComparer.OrdinalIgnoreCase);
        public string Body { get; set; } = "";
    }

    public class MimeMessage
    {
        public Dictionary<string, string> Headers { get; set; } = new(StringComparer.OrdinalIgnoreCase);
        public List<MimePart> Parts { get; set; } = new();
        public string Body { get; set; } = "";
        public string ContentType => Headers.TryGetValue("Content-Type", out var ct) ? ct : null;
    }

    public class MimeParser
    {
        public MimeMessage Parse(string rawInput)
        {
            var message = new MimeMessage();
            var lines = rawInput.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
            int index = 0;

            // Step 1: Parse headers
            var headerBuilder = new StringBuilder();
            while (index < lines.Length && string.IsNullOrWhiteSpace(lines[index]))
                index++;

            while (index < lines.Length)
            {
                var line = lines[index];
                if (string.IsNullOrWhiteSpace(line))
                {
                    index++;
                    break;
                }

                if (line.StartsWith(" ") || line.StartsWith("\t"))
                {
                    headerBuilder.AppendLine(line);
                }
                else
                {
                    headerBuilder.AppendLine(line);
                }

                index++;
            }

            var headerLines = headerBuilder.ToString().Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
            string currentKey = null;
            foreach (var line in headerLines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                if (char.IsWhiteSpace(line[0]) && currentKey != null)
                {
                    message.Headers[currentKey] += " " + line.Trim();
                }
                else
                {
                    var colon = line.IndexOf(':');
                    if (colon > 0)
                    {
                        currentKey = line.Substring(0, colon).Trim();
                        var value = line.Substring(colon + 1).Trim();
                        message.Headers[currentKey] = value;
                    }
                }
            }

            var bodyBuilder = new StringBuilder();
            while (index < lines.Length)
            {
                bodyBuilder.AppendLine(lines[index]);
                index++;
            }
            message.Body = bodyBuilder.ToString().Trim();

            if (message.ContentType?.StartsWith("multipart/", StringComparison.OrdinalIgnoreCase) == true)
            {
                var boundaryMatch = Regex.Match(message.ContentType, "boundary=\"?([^\"]+)\"?");
                if (boundaryMatch.Success)
                {
                    var boundary = "--" + boundaryMatch.Groups[1].Value;
                    var sections = message.Body.Split(new[] { boundary }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var section in sections)
                    {
                        var clean = section.Trim();
                        if (clean == "--" || string.IsNullOrWhiteSpace(clean)) continue;
                        message.Parts.Add(ParsePart(clean));
                    }
                }
            }

            return message;
        }

        private MimePart ParsePart(string partContent)
        {
            var part = new MimePart();
            var lines = partContent.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
            int index = 0;

            // Headers
            var headerBuilder = new StringBuilder();
            while (index < lines.Length)
            {
                var line = lines[index];
                if (string.IsNullOrWhiteSpace(line))
                {
                    index++;
                    break;
                }

                headerBuilder.AppendLine(line);
                index++;
            }

            // Parse headers
            var headerLines = headerBuilder.ToString().Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
            string currentKey = null;
            foreach (var line in headerLines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                if (char.IsWhiteSpace(line[0]) && currentKey != null)
                {
                    part.Headers[currentKey] += " " + line.Trim();
                }
                else
                {
                    var colon = line.IndexOf(':');
                    if (colon > 0)
                    {
                        currentKey = line.Substring(0, colon).Trim();
                        var value = line.Substring(colon + 1).Trim();
                        part.Headers[currentKey] = value;
                    }
                }
            }

            // Body
            var bodyBuilder = new StringBuilder();
            while (index < lines.Length)
            {
                bodyBuilder.AppendLine(lines[index]);
                index++;
            }

            part.Body = bodyBuilder.ToString().Trim();
            return part;
        }
    }
}
