using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Beetle.Redis
{

    public class  RedisKey
    {
        public RedisKey(string datakey,DataType dtype)
        {
            DataKey = datakey;
            DataType = dtype;
            mIsSingleKey = true;
        }

        private bool mIsSingleKey = true;

        public RedisKey(IEnumerable<string> datakey, DataType dtype)
        {
            DataKey = datakey;
            DataType = dtype;
            mIsSingleKey = false;
        }

        protected object DataKey
        {
            get;
            set;
        }

        private string[] GetKeys
        {
            get
            {
                return ((IEnumerable<string>)DataKey).ToArray();
            }
        }

        public void Delete( RedisClient db = null)
        {
            db = RedisClient.GetClient(db);
            if (mIsSingleKey)
                db.Delete((string)DataKey);
            else
                db.Delete(GetKeys);
        }

        public IList<string> Keys( RedisClient db = null)
        {
            db = RedisClient.GetClient(db);
            return db.Keys((string)DataKey);
        }

        protected DataType DataType
        {
            get;
            private set;
        }

        public T Get<T>(RedisClient db = null)
        {
            db = RedisClient.GetClient(db);
            return db.Get<T>((string)DataKey, DataType);
        }

        public void Set(object value, RedisClient db = null)
        {
            db = RedisClient.GetClient(db);
            db.Set((string)DataKey, value, DataType);
        }

        public void SetValues(params object[] values)
        {
            SetValues(values, null);
        }

        public void SetValues(object[] values, RedisClient db = null)
        {
            db = RedisClient.GetClient(db);
            List<Field> kvs = new List<Field>();
            string[] keys = GetKeys;
            int count = keys.Length > values.Length ? values.Length : keys.Length;
            for (int i = 0; i < count; i++)
                kvs.Add(new Field { Value=values[i], Name=keys[i] });
            db.Set(kvs.ToArray(), DataType);
        }

        public IList<object> Get(params Type[] types)
        {
            return Get(types, null);
        }

        public IList<object> Get(Type[] types, RedisClient db = null)
        {
            db = RedisClient.GetClient(db);
            return db.Get(types, GetKeys, DataType);
        }

        public IList<object> Get<T, T1>(RedisClient db = null)
        {

            db = RedisClient.GetClient(db);
            return db.Get(new Type[] {typeof(T),typeof(T1) }, GetKeys, DataType);
           
        }

        public IList<object> Get<T, T1, T2>(RedisClient db = null)
        {
            db = RedisClient.GetClient(db);
            return db.Get(new Type[] { typeof(T), typeof(T1),typeof(T2) }, GetKeys, DataType);
        }

        public IList<object> Get<T, T1, T2, T3>(RedisClient db = null)
        {
            db = RedisClient.GetClient(db);
            return db.Get(new Type[] { typeof(T), typeof(T1), typeof(T2),typeof(T3) }, GetKeys, DataType);
        }

        public IList<object> Get<T, T1, T2, T3, T4>(RedisClient db = null)
        {
            db = RedisClient.GetClient(db);
            return db.Get(new Type[] { typeof(T), typeof(T1), typeof(T2), typeof(T3),typeof(T4) }, GetKeys, DataType);
        }

        #region lst     
        public int LstCount(RedisClient db = null)
        {
            db = RedisClient.GetClient(db);
            return db.ListLength((string)DataKey);
        }

        public T LstPop<T>(RedisClient db = null)
        {
            db = RedisClient.GetClient(db);
            return db.ListPop<T>((string)DataKey, DataType);
        }

        public T LstRemove<T>(RedisClient db = null)
        {
            db = RedisClient.GetClient(db);
            return db.ListRemove<T>((string)DataKey, DataType);
        }

        public void LstAdd(object value, RedisClient db = null)
        {
            db = RedisClient.GetClient(db);
            db.ListAdd((string)DataKey, value, DataType);
        }

        public void LstPush(object value, RedisClient db = null)
        {
            db = RedisClient.GetClient(db);
            db.ListPush((string)DataKey, value, DataType);
        }

        public IList<T> LstRange<T>(int start, int end, RedisClient db = null)
        {
            db = RedisClient.GetClient(db);
            return db.ListRange<T>((string)DataKey, start, end, DataType);
        }

        public IList<T> LstRange<T>(RedisClient db = null)
        {
            db = RedisClient.GetClient(db);
            return db.ListRange<T>((string)DataKey, DataType);
        }

        public T LstGetItem<T>(int index, RedisClient db = null)
        {
            db = RedisClient.GetClient(db);
            return db.GetListItem<T>((string)DataKey, index, DataType);
        }

        public void LstSetItem(int index,object value, RedisClient db = null)
        {
            db = RedisClient.GetClient(db);
            db.SetListItem((string)DataKey, index, value, DataType);
        }

        #endregion
        #region field
        public  IList<object> GetFields<T>( RedisClient db = null)
        {
            db = RedisClient.GetClient(db);
            return db.GetFields((string)DataKey, new NameType[] {
            new NameType(typeof(T))}, DataType).FieldValueToList();
        }

        public  IList<object> GetFields<T, T1>( RedisClient db = null)
        {
            db = RedisClient.GetClient(db);
            return db.GetFields((string)DataKey, new NameType[] {
            new NameType(typeof(T)),new NameType(typeof(T1))}, DataType).FieldValueToList();
        }

        public  IList<object> GetFields<T, T1, T2>( RedisClient db = null)
        {
            db = RedisClient.GetClient(db);
            return db.GetFields((string)DataKey, new NameType[] {
            new NameType(typeof(T))
            ,new NameType(typeof(T1))
            ,new NameType(typeof(T2))}, DataType).FieldValueToList();
        }

        public  IList<object> GetFields<T, T1, T2, T3>( RedisClient db = null)
        {
            db = RedisClient.GetClient(db);
            return db.GetFields((string)DataKey, new NameType[] {
            new NameType(typeof(T))
            ,new NameType(typeof(T1))
            ,new NameType(typeof(T2))
            ,new NameType(typeof(T3))}, DataType).FieldValueToList();
        }

        public  IList<object> GetFields<T, T1, T2, T3, T4>( RedisClient db = null)
        {
            db = RedisClient.GetClient(db);
            return db.GetFields((string)DataKey, new NameType[] {
            new NameType(typeof(T))
            ,new NameType(typeof(T1))
            ,new NameType(typeof(T2))
            ,new NameType(typeof(T3)),
            new NameType(typeof(T4))}, DataType).FieldValueToList();
        }

        public void SetField(string field, object value, RedisClient db = null)
        {
            SetField(new Field { Name = field, Value = value }, db);
        }

        public void SetField(Field field, RedisClient db = null)
        {
            SetField(new Field[] { field }, db);
        }

        public void SetField(params object[] values)
        {
            SetField(values, null);
        }

        public void SetField(object[] value, RedisClient db = null)
        {
         
            List<Field> fields = new List<Field>();
            foreach(object item in value)
            {
                fields.Add(new Field{ Name= item.GetType().Name,Value= item});
            }
            SetField(fields.ToArray(), db);
        }

        public void SetField(IList<object> value,RedisClient db = null)
        {
            SetField(value.ToArray(),db);
        }

        public void SetField(params Field[] fields)
        {
            Set(fields, null);
        }

        public void SetField(Field[] fields, RedisClient db = null)
        {
            db = RedisClient.GetClient(db);
            db.SetFields((string)DataKey, fields, DataType);
        }
        #endregion


    }


}
