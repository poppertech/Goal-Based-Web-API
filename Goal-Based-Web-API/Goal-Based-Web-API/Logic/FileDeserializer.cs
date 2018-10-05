using CsvHelper;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Api.Logic
{
    public interface IFileDeserializer
    {
        void ReadFile(IFormFile file);
    }

    public abstract class FileDeserializerBase: IFileDeserializer
    {
        public void ReadFile(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                memoryStream.Position = 0;
                using (var streamReader = new StreamReader(memoryStream))
                {
                    Deserialize(streamReader);
                }
            }
        }

        protected abstract void Deserialize(TextReader textReader);
    }
    public interface ICsvFileDeserializer<T>:IFileDeserializer
    {
        IList<T> GetRecords();
    }

    public class CsvFileDeserializer<T> : FileDeserializerBase, ICsvFileDeserializer<T>
    {
        private IList<T> _records;
        public IList<T> GetRecords()
        {
            return _records;
        }

        protected override void Deserialize(TextReader textReader)
        {
            using (var csv = new CsvReader(textReader))
            {
                csv.Configuration.HasHeaderRecord = true;
                _records = csv.GetRecords<T>().ToList();
            }
        }
    }

    public interface IJsonFileDeserializer<T>:IFileDeserializer
    {
        T GetInstance();
    }

    public class JsonFileDeserializer<T>: FileDeserializerBase, IJsonFileDeserializer<T>
    {
        private T _instance;

        public T GetInstance()
        {
            return _instance;
        }

        protected override void Deserialize(TextReader textReader)
        {
            using (var jsonReader = new JsonTextReader(textReader))
            {
                var serializer = new JsonSerializer();
                _instance = serializer.Deserialize<T>(jsonReader);
            }
        }
    }




}