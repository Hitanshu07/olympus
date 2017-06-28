using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using WebInkLibrary.Core;
using WebInkLibrary.Utils.SerialNumber;

namespace WebInkLibrary.Utils
{
    //update using https://github.com/loresoft/EntityFramework.Extended
    public class ModuleService<T> : ISerialNoService<T> where T : BaseEntity, new()
    {
        private readonly DbContext _ctx;

        public ModuleService(DbContext dbContext)
        {
            _ctx = dbContext;
        }

        public virtual T AddSerialNumber(T entity, int srNo)//InsertEntity
        {
            entity.SrNo = srNo;
            var addAtSerialNo = srNo;
            _entities = _ctx.Set<T>();

            var orderedEntities = _entities.Where(t => t.SrNo >= addAtSerialNo);
            foreach (var entityItem in orderedEntities)
            {
                entityItem.SrNo += 1;
            }
            _entities.AddOrUpdate(entity);
            _ctx.SaveChanges();
            return entity;
        }
        public virtual T AddSerialNumberByPredicate(T entity, int srNo, Expression<Func<T, Boolean>> predicate)//InsertEntity
        {
            entity.SrNo = srNo;
            var addAtSerialNo = srNo;
            _entities = _ctx.Set<T>();

            var orderedEntities = _entities.Where(predicate).Where(t => t.SrNo >= addAtSerialNo);
            foreach (var entityItem in orderedEntities)
            {
                entityItem.SrNo += 1;
            }
            _entities.AddOrUpdate(entity);
            _ctx.SaveChanges();
            return entity;
        }


        public T RemoveSerialNumberByPredicate(T entity, Expression<Func<T, Boolean>> predicate)
        {
            var removeSerialNo = entity.SrNo;
            _entities = _ctx.Set<T>();

            var orderedEntities = _entities.Where(predicate).Where(t => t.SrNo > removeSerialNo & t.SrNo > 0);
            foreach (var entityItem in orderedEntities)
            {
                entityItem.SrNo = entityItem.SrNo - 1;
            }
            //_entities.Remove(entity);
            _ctx.SaveChanges();
            return entity;
        }
        public T RemoveSerialNumber(T entity)
        {
            var removeSerialNo = entity.SrNo;
            _entities = _ctx.Set<T>();

            var orderedEntities = _entities.Where(t => t.SrNo > removeSerialNo & t.SrNo > 0);
            foreach (var entityItem in orderedEntities)
            {
                entityItem.SrNo = entityItem.SrNo - 1;
            }
            //_entities.Remove(entity);
            _ctx.SaveChanges();
            return entity;
        }

        public T MoveSerialNumber(int targetSerialNo, T entity)
        {
            var oldSrno = entity.SrNo;
            _entities = _ctx.Set<T>();
            if (oldSrno < targetSerialNo)
            {
                var orderedEntities = _entities.Where(t => t.SrNo <= targetSerialNo & t.SrNo > oldSrno);
                foreach (var entityItem in orderedEntities)
                {
                    entityItem.SrNo = entityItem.SrNo - 1;
                }
                entity.SrNo = targetSerialNo;
                _entities.AddOrUpdate(entity);
                _ctx.SaveChanges();
                return entity;
            }
            else
            {
                var orderedEntities = _entities.Where(t => t.SrNo >= targetSerialNo & t.SrNo < oldSrno);
                foreach (var entityItem in orderedEntities)
                {
                    entityItem.SrNo = entityItem.SrNo + 1;
                }
                entity.SrNo = targetSerialNo;
                _entities.AddOrUpdate(entity);
                _ctx.SaveChanges();
                return entity;
            }
        }
        //predicate Expression<Func<MyEntity, bool>> predicate = x => x.Age > 18;

        public T MoveSerialNumberByPredicate(int targetSerialNo, T entity, Expression<Func<T, Boolean>> predicate)
        {
            var oldSrno = entity.SrNo;
            _entities = _ctx.Set<T>();
            if (oldSrno < targetSerialNo)
            {
                var orderedEntities = _entities.Where(predicate).Where(t => t.SrNo <= targetSerialNo & t.SrNo > oldSrno);
                foreach (var entityItem in orderedEntities)
                {
                    entityItem.SrNo = entityItem.SrNo - 1;
                }
                entity.SrNo = targetSerialNo;
                _entities.AddOrUpdate(entity);
                _ctx.SaveChanges();
                return entity;
            }
            else
            {
                var orderedEntities = _entities.Where(predicate).Where(t => t.SrNo >= targetSerialNo & t.SrNo < oldSrno);
                foreach (var entityItem in orderedEntities)
                {
                    entityItem.SrNo = entityItem.SrNo + 1;
                }
                entity.SrNo = targetSerialNo;
                _entities.AddOrUpdate(entity);
                _ctx.SaveChanges();
                return entity;
            }
        }

        private IDbSet<T> _entities;
        public string GetCleanedFileName(string fileName)
        {
            if (fileName == null || fileName == "") return "";
            int len = fileName.Length;
            const int maxlen = 100;

            var ext = fileName.Substring((len - 4), 4);
            bool prevdash = false;
            var sb = new StringBuilder(len);
            char c;

            for (int i = 0; i < len; i++)
            {
                c = fileName[i];
                if ((c >= 'a' && c <= 'z') || (c >= '0' && c <= '9'))
                {
                    sb.Append(c);
                    prevdash = false;
                }
                else if (c >= 'A' && c <= 'Z')
                {
                    // tricky way to convert to lowercase
                    sb.Append((char)(c | 32));
                    prevdash = false;
                }
                else if (c == ' ' || c == ',' || c == '/' ||
                    c == '\\' || c == '-' || c == '_' || c == '=')
                {
                    if (!prevdash && sb.Length > 0)
                    {
                        sb.Append('-');
                        prevdash = true;
                    }
                }
                else if ((int)c >= 128)
                {
                    int prevlen = sb.Length;
                    sb.Append(RemapInternationalCharToAscii(c));
                    if (prevlen != sb.Length) prevdash = false;
                }
                if (i == maxlen) break;
            }

            if (prevdash)
            {
                return String.Concat(sb.ToString().Substring(0, sb.Length - 1), ext);
            }
            else
                return String.Concat(sb.ToString(), ext);
        }


        public static string RemapInternationalCharToAscii(char c)
        {
            string s = c.ToString().ToLowerInvariant();
            if ("àåáâäãåą".Contains(s))
            {
                return "a";
            }
            else if ("èéêëę".Contains(s))
            {
                return "e";
            }
            else if ("ìíîïı".Contains(s))
            {
                return "i";
            }
            else if ("òóôõöøőð".Contains(s))
            {
                return "o";
            }
            else if ("ùúûüŭů".Contains(s))
            {
                return "u";
            }
            else if ("çćčĉ".Contains(s))
            {
                return "c";
            }
            else if ("żźž".Contains(s))
            {
                return "z";
            }
            else if ("śşšŝ".Contains(s))
            {
                return "s";
            }
            else if ("ñń".Contains(s))
            {
                return "n";
            }
            else if ("ýÿ".Contains(s))
            {
                return "y";
            }
            else if ("ğĝ".Contains(s))
            {
                return "g";
            }
            else if (c == 'ř')
            {
                return "r";
            }
            else if (c == 'ł')
            {
                return "l";
            }
            else if (c == 'đ')
            {
                return "d";
            }
            else if (c == 'ß')
            {
                return "ss";
            }
            else if (c == 'Þ')
            {
                return "th";
            }
            else if (c == 'ĥ')
            {
                return "h";
            }
            else if (c == 'ĵ')
            {
                return "j";
            }
            else
            {
                return "";
            }
        }
    }

}
