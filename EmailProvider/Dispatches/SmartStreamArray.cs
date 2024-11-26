using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EmailProvider.Dispatches
{
    public class SmartStreamArray : IDisposable
    {
        private MemoryStream _stream;
        private BinaryWriter _writer;
        private BinaryReader _reader;

        public SmartStreamArray()
        {
            Reset();
        }

        ~SmartStreamArray()
        {
            Dispose();   
        }

        /// <summary> Сериализираме обкет към масив от байтове и го записваме в stream-а, реда на сериализиране и десериализиране трябва да е еднакъв!!! </summary>
        public void Serialize(object obj)
        {
            byte[] objectBytes = JsonSerializer.SerializeToUtf8Bytes(obj);

            //записваме размера
            _writer.Write(objectBytes.Length);
            
            //записваме същинските данни
            _writer.Write(objectBytes);
        }

        /// <summary> Изчитаме масива от байтове от потока и го десериализираме в обекта, реда на сериализиране и десериализиране трябва да е еднакъв!!! </summary>
        public void Deserialize<T>(out T obj)
        {
            //Местим се до следваща позиция за четене
            _stream.Position = _reader.BaseStream.Position;

            //първо изчитаме размера на обекта който ще десериализираме
            int length = _reader.ReadInt32();

            //Четем същинските данни
            byte[] objectBytes = _reader.ReadBytes(length);

            //Десериализираме от масива от байтове
            obj = JsonSerializer.Deserialize<T>(objectBytes);
        }

        /// <summary> Подготвяме за изпращане </summary>
        public byte[] ToByte()
        {
            byte[] payload = _stream.ToArray();
            int totalLength = payload.Length;

            using (MemoryStream resultStream = new MemoryStream())
            {
                using (BinaryWriter resultWriter = new BinaryWriter(resultStream))
                {
                    // пишем размера на всички данни
                    resultWriter.Write(totalLength);

                    // пешем същинските данни
                    resultWriter.Write(payload);
                }

                return resultStream.ToArray();
            }
        }

        // зареждаме потока от масив
        public void ToArray(byte[] data)
        {
            _stream = new MemoryStream(data);
            _reader = new BinaryReader(_stream);
            _writer = new BinaryWriter(_stream);
        }

        public void Dispose()
        {
            _stream?.Dispose();
            _writer?.Dispose();
            _reader?.Dispose();
            GC.SuppressFinalize(this);
        }

        public void Reset()
        {
            _stream = new MemoryStream();
            _reader = new BinaryReader(_stream);
            _writer = new BinaryWriter(_stream);
        }

        public void LoadFromStream(Stream networkStream)
        {
            byte[] lengthPrefix = new byte[4];
            int totalBytesRead = 0;

            // Изчитаме колко ще е голяма заявката
            while (totalBytesRead < 4)
            {
                int bytesRead = networkStream.Read(lengthPrefix, totalBytesRead, 4 - totalBytesRead);
                if (bytesRead == 0) throw new IOException("Disconnected before reading the length prefix.");
                totalBytesRead += bytesRead;
            }

            int messageLength = BitConverter.ToInt32(lengthPrefix, 0);

            if (messageLength <= 0 || messageLength > 10_000_000) // Validate message length
                throw new IOException($"Invalid message length: {messageLength}");

            // чедтем същинските данни
            byte[] payload = new byte[messageLength];
            totalBytesRead = 0;
            while (totalBytesRead < messageLength)
            {
                int bytesRead = networkStream.Read(payload, totalBytesRead, messageLength - totalBytesRead);
                if (bytesRead == 0) throw new IOException("Disconnected before reading the full payload.");
                totalBytesRead += bytesRead;
            }

            // Зареждаме си само същинските данни
            ToArray(payload);
        }
    }
}
