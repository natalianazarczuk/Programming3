using System;
using System.IO;
using System.Xml.Serialization;

namespace Lab13
{
    [Serializable]
    public class Database
    {
        private readonly string _baseDir;

        public Database(string baseDir)
        {
            _baseDir = baseDir;

            if (!Directory.Exists(_baseDir))
            {
                Directory.CreateDirectory(_baseDir);
            }
        }

        /// <summary>
        /// Adds new object to the database.
        /// If the object with the given Id and the same type exists, the exception should be thrown.
        /// If Id == 0 the value of Id should be replaced by te max Id value for TEntity object type + 1
        /// </summary>

        // TODO: Implement Add method
        public void Add<TEntity>(TEntity entity) where TEntity : IEntity
        {
            if (entity.Id == 0)
            {
                var directory = new DirectoryInfo(_baseDir);
                int id = 0;
                foreach (var file in directory.EnumerateFiles($"*{typeof(TEntity).Name}*"))
                {
                    int temp = Int32.Parse(file.Name.Split("_")[1].Split(".")[0]);
                    if (temp > id) id = temp;
                }
                entity.Id = id + 1;
            }

            string path = $"{_baseDir}\\{typeof(TEntity).Name}_{entity.Id}.xml";
            if (File.Exists(path))
                throw new Exception("File exists");
            
            var serializer = new XmlSerializer(typeof(TEntity));
            using var fs = new FileStream(path, FileMode.Create);
            serializer.Serialize(fs, entity);
        }


        /// <summary>
        /// Get retrieves an object from the database. 
        /// If the object with the given Id does not exist, the exception should be thrown.
        /// </summary>

        // TODO: Implement Get method
        public TEntity Get<TEntity>(int id) where TEntity : IEntity
        {
            string path = $"{_baseDir}\\{typeof(TEntity).Name}_{id}.xml";
            if (!File.Exists(path))
                throw new Exception("File doesn't exist");

            var serializer = new XmlSerializer(typeof(TEntity));
            using var fs = new FileStream(path, FileMode.Open);

            return (TEntity)serializer.Deserialize(fs);
        }

        /// <summary>
        /// Updates object in the database.
        /// If the object with the given Id does not exist, the exception should be thrown.
        /// </summary>

        // TODO: Implement Update method
        public void Update<TEntity>(TEntity entity) where TEntity : IEntity
        {
            string path = $"{_baseDir}\\{typeof(TEntity).Name}_{entity.Id}.xml";
            if (!File.Exists(path))
                throw new Exception("File doesn't exist");

            File.Delete(path);
            Add<TEntity>(entity); 
        }

        /// <summary>
        /// Deletes object from the database.
        /// If the object with the given Id does not exist, the exception should be thrown.
        /// </summary>

        // TODO: Implement Delete method
        public void Delete<TEntity>(int id) where TEntity : IEntity
        {
            string path = $"{_baseDir}\\{typeof(TEntity).Name}_{id}.xml";
            if (!File.Exists(path))
                throw new Exception("File doesn't exist");

            File.Delete(path);
        }
    }

    [Serializable]
    public class User : IEntity
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        /// <summary>
        /// Property computed from FirstName and LastName.
        /// It should not be serialized to the file.
        /// </summary>
        [XmlIgnore]
        public string FullName => $"{FirstName} {LastName}";

        public override string ToString()
        {
            return $"USER: Id {Id}, FullName: {FullName}";
        }
    }

    [Serializable]
    public class Product : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return $"PRODUCT: Id: {Id}, Name {Name}";
        }
    }

    [Serializable]
    public class Order : IEntity
    {
        public Order(User user, Product product, int amount)
        {
            UserId = user.Id;
            ProductId = product.Id;
            Amount = amount;
        }
        private Order() { }

        public int Id { get; set; }

        public int UserId { get; set; }

        public int ProductId { get; set; }

        public int Amount { get; set; }

        public override string ToString()
        {
            return $"ORDER: Id {Id}, UserId: {UserId}, ProductId: {ProductId}, Amount: {Amount}";
        }
    }
}