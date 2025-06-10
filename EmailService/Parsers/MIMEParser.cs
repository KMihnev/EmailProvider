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

            var headerLines = new List<string>();
            while (index < lines.Length)
            {
                var line = lines[index];

                if (string.IsNullOrWhiteSpace(line))
                {
                    index++;
                    break;
                }

                if (!line.Contains(':') && !char.IsWhiteSpace(line[0]))
                {
                    break;
                }

                headerLines.Add(line);
                index++;
            }

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
                        if (!message.Headers.ContainsKey(currentKey))
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

            // Step 3: Handle multipart
            if (message.ContentType?.StartsWith("multipart/", StringComparison.OrdinalIgnoreCase) == true)
            {
                var boundaryMatch = Regex.Match(message.ContentType, @"boundary=""?([^"";]+)""?");
                if (boundaryMatch.Success)
                {
                    var boundary = "--" + boundaryMatch.Groups[1].Value;
                    var closingBoundary = boundary + "--";

                    var normalizedBody = message.Body.Replace("\r\n", "\n").Replace("\r", "\n");
                    var parts = Regex.Split(normalizedBody, $@"\n{Regex.Escape(boundary)}\s*");

                    foreach (var rawPart in parts)
                    {
                        var clean = rawPart.Trim();

                        // Skip only true end marker like "--boundary--"
                        if (clean.Equals("--", StringComparison.Ordinal)) continue;

                        // Remove leading boundary line if present
                        if (clean.StartsWith("--"))
                        {
                            var firstNewline = clean.IndexOf('\n');
                            if (firstNewline > 0)
                            {
                                clean = clean.Substring(firstNewline + 1).TrimStart();
                            }
                        }

                        if (string.IsNullOrEmpty(clean)) continue;

                        var part = ParsePart(clean);
                        if (part != null)
                            message.Parts.Add(part);
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
            var headerLines = new List<string>();
            while (index < lines.Length)
            {
                var line = lines[index];
                if (string.IsNullOrWhiteSpace(line))
                {
                    index++;
                    break;
                }

                if (!line.Contains(':') && !char.IsWhiteSpace(line[0]))
                {
                    break;
                }

                headerLines.Add(line);
                index++;
            }

            // Parse headers
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
                        if (!part.Headers.ContainsKey(currentKey))
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
