using PassTrackingSystem.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PassTrackingSystem.Infrastructure
{
    public class RandomDataGenerator
    {
        private ApplicationDBContext _dBContext;
        public RandomDataGenerator(ApplicationDBContext dBContext)
        {
            _dBContext = dBContext;
            AddInitDataToDB();
        }

        private void CreateDocumentTypes()
        {
            var values = new List<string> { "Паспорт",
               "Водительское удостовириение",
               "Удостоверение личности"};

            values.ForEach(val => _dBContext.DocumentTypes.Add(new DocumentType { Value = val }));
            _dBContext.SaveChanges();
        }


        private void CreateIssuingAuthority()
        {
            var values = new List<string> { "КГБ",
               "РОВД",
               "МВД",
            "ГАИ"};
            values.ForEach(val => _dBContext.IssuingAuthorities.Add(new IssuingAuthority { Value = val }));
            _dBContext.SaveChanges();
        }

        public void CreateVisitors(int quantity)
        {
            var n = getRandomStringFromFile(@"\Resource\Names.txt");

        }

        
        public DocumentType GenerateRandom()
        {
            return null;
        }


        private string getRandomStringFromFile(string Path)
        {
            using (StreamReader sr = new StreamReader(Path))
            {
                var allFileString = sr.ReadToEnd();
                var splited = allFileString.Split("\r\n").ToList();
                if (splited.Count > 0)
                {
                    return splited[new Random(splited.Count - 1).Next()];
                }

                throw new InvalidOperationException("failed to process file");
            }
        }

        private void AddInitDataToDB()
        {
            CreateDocumentTypes();
            CreateIssuingAuthority();
        }

    }




}
