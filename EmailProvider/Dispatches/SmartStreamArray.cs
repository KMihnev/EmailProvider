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
            _stream = new MemoryStream();
            _writer = new BinaryWriter(_stream);
            _reader = new BinaryReader(_stream);
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
            return _stream.ToArray();
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
    }
}
