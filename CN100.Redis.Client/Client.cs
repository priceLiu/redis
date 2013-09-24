using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CN100.ProductDetail.Common.Log;
using Beetle.Redis;

namespace CN100.Redis.Client
{
    public class RedisClientUtility
    {
        RedisClientUtility() { }

        //private RedisClient db = null;

        //public Client(string connectName)
        //{
        //    try
        //    {
        //        db = new RedisClient(connectName);
        //    }
        //    catch(Exception ex)
        //    {
        //        string logstr = "Redis客户端初始化错误，请检查配置参数是否正确！";
        //        Logger.RedisLog.Fatal(logstr, ex);
        //        throw new Exception(logstr, ex);
        //    }
        //}


        #region 数据写入

        /// <summary>
        /// 列表数据写入（如果列表不存在，则先创建，如果存在，则Append）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        public static void SetDataList<T>(string key, IList<T> obj)
        {
            //检查非法输入
            if (string.IsNullOrWhiteSpace(key)) return;
            if (obj == null || obj.Count <= 0) return;


            try
            {
                RedisKey rk = key.RedisProtobuf();
                foreach(T item in obj)
                    rk.LstPush(item);
            }
            catch(Exception ex)
            {
                string errstr = "数据写入失败,Method=SetDataList<T>(string key, IList<T> obj)，key=" + key + "，内容：" + string.Join(",", obj);
                Logger.RedisLog.Error(errstr, ex);
                throw ex;
            }
        }

        /// <summary>
        /// 单个对象写入（如果不存在，则先创建后写入，如果存在，则Update）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <returns>true写入成功false写入失败</returns>
        public static void SetData<T>(string key, T obj)
        {
            //检查非法输入
            if (string.IsNullOrWhiteSpace(key)) return;
            if (obj == null) return;

            try
            {
                RedisKey rk = key.RedisProtobuf();
                rk.Set(obj);
            }
            catch (Exception ex)
            {
                string errstr = "数据写入失败,Method=SetData<T>(string key, T obj)，key=" + key + "，内容：" + obj.ToString();
                Logger.RedisLog.Error(errstr, ex);
                throw ex;
            }
        }

        /// <summary>
        /// 数据写入
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <returns>true写入成功false写入失败</returns>
        public static void SetData(string key, string data)
        {
            //检查非法输入
            if (string.IsNullOrWhiteSpace(key)) return;
            if (string.IsNullOrWhiteSpace(data)) return;

            SetData<string>(key, data);
        }


        /// <summary>
        /// 一次写入多个key-val键值对
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        public static void SetData<T>(Dictionary<string, T> obj)
        {
            //检查非法输入
            if (obj == null || obj.Count <= 0) return;

            try
            {
                foreach (KeyValuePair<string, T> item in obj)
                {
                    RedisKey rk = item.Key.RedisProtobuf();
                    rk.Set(obj);
                }
            }
            catch (Exception ex)
            {
                string errstr = "数据写入失败,Method=SetData<T>(Dictionary<string, T> obj)，内容：" + string.Join(",", obj);
                Logger.RedisLog.Error(errstr, ex);
                throw ex;
            }
        }

        #endregion

        #region 数据获取

        /// <summary>
        /// 一次获取多个键值对
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keys"></param>
        /// <returns></returns>
        public static IList<object> GetDataByKeys(Dictionary<string, Type> obj)
        {
            //检查非法输入
            if (obj == null || obj.Count <= 0) return null;

            List<Type> types = new List<Type>();
            List<string> keys = new List<string>();
            try
            {
                 

                foreach (KeyValuePair<string, Type> item in obj)
                {
                    types.Add(item.Value);
                    keys.Add(item.Key);
                }

                RedisKey rk = keys.RedisProtobuf();
                return rk.Get(types.ToArray());
            }
            catch (Exception ex)
            {
                string errstr = "获取数据失败,Method=GetDataByKeys(Dictionary<string,Type> obj)，key=" + string.Join(",", keys);
                Logger.RedisLog.Error(errstr, ex);
                throw ex;
            }
        }

        /// <summary>
        /// 获取单个对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetData(string key)
        {
            //检查非法输入
            if (string.IsNullOrWhiteSpace(key)) return string.Empty;

            try
            {
                return GetData<string>(key);
            }
            catch (Exception ex)
            {
                string errstr = "获取数据失败,Method=GetData(string key)，key=" + string.Join(",", key);
                Logger.RedisLog.Error(errstr, ex);
                throw ex;
            }
        }

        /// <summary>
        /// 获取单个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetData<T>(string key)
        {
            //检查非法输入
            if (string.IsNullOrWhiteSpace(key)) return default(T);

            try
            {
                RedisKey rk = key.RedisProtobuf();
                return rk.Get<T>();
            }
            catch (Exception ex)
            {
                string errstr = "获取数据失败,Method=GetData<T>(string key)，key=" + string.Join(",", key) + "，Stack=" + ex.StackTrace;
                Logger.RedisLog.Error(errstr, ex);
                throw ex;
            }
        }

        /// <summary>
        /// 范围获取Redis列表数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="fromIndex">范围开始值（注：Redis的Index从0开始）</param>
        /// <param name="toIndex">范围结束值</param>
        /// <returns></returns>
        public static IList<T> GetDataListByRange<T>(string key, int fromIndex, int toIndex)
        {
            if (string.IsNullOrWhiteSpace(key)) return null;

            try
            {
                RedisKey rk = key.RedisProtobuf();
                return rk.LstRange<T>(fromIndex, toIndex);
            }
            catch (Exception ex)
            {
                string errstr = "获取数据失败,Method=GetDataListByRange<T>(string key, int fromIndex, int toIndex)，key=" + key + ",fromIndex=" + fromIndex.ToString() + ",toIndex=" + toIndex + "";
                Logger.RedisLog.Error(errstr, ex);
                throw ex;
            }
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static IList<T> GetDataList<T>(string key)
        {
            //非法输入检查
            if (string.IsNullOrWhiteSpace(key)) return null;

            int reccount = 0;
            return GetDataList<T>(key, 0, int.MaxValue, out reccount);
        }


        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="pageIndex">起始页</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="recordCount">返回记录总数</param>
        /// <returns>返回对象列表</returns>
        public static IList<T> GetDataList<T>(string key, int pageIndex, int pageSize, out int recordCount)
        {
            recordCount = 0;  //返回记录数

            //非法输入检查
            if (string.IsNullOrWhiteSpace(key)) return null;

            int fromIndex = pageIndex - 1;  //redis的index是从0开始的
            int toIndex = pageSize - 1;

            RedisKey rk = key.RedisProtobuf();
            recordCount = rk.LstCount();


            if (fromIndex > 0)
            {
                fromIndex = (fromIndex * toIndex) + (pageIndex - 1);
                toIndex = fromIndex + toIndex;
            }

            if (fromIndex < 0) fromIndex = 0;

            IList<T> userlist = GetDataListByRange<T>(key, fromIndex, toIndex);
            return userlist;
        }

        #endregion

        #region 删除

        /// <summary>
        /// 删除一个key
        /// </summary>
        /// <param name="key"></param>
        public static void Del(string key)
        {
            if (string.IsNullOrWhiteSpace(key)) return;

            try
            {
                Del(new List<string>() { key });
            }
            catch (Exception ex)
            {
                string errstr = "删除数据失败,Method=Del(string key)，key=" + key;
                Logger.RedisLog.Error(errstr, ex);
                throw ex;
            }
        }

        /// <summary>
        /// 一次删除多个key
        /// </summary>
        /// <param name="keys"></param>
        public static void Del(List<string> keys)
        {
            if (keys == null || keys.Count <= 0) return;

            try
            {
                RedisKey rk = keys.RedisProtobuf();
                rk.Delete();
            }
            catch (Exception ex)
            {
                string errstr = "删除数据失败,Method=Del(List<string> keys),key=" + string.Join(",", keys);
                Logger.RedisLog.Error(errstr, ex);
                throw ex;
            }
        }


        /// <summary>
        /// 模糊查询Key并删除（请慎用）
        /// </summary>
        /// <param name="keysPattern"></param>
        public static void DelBySearchKey(string keysPattern)
        {
            if (string.IsNullOrWhiteSpace(keysPattern)) return;

            try
            {
                RedisKey rk = keysPattern.RedisProtobuf();
                IList<string> keyslist = rk.Keys();
                Del(keyslist.ToList());
            }
            catch (Exception ex)
            {
                string errstr = "删除数据失败,Method=DelBySearchKey(string keysPattern)，keysPattern=" + keysPattern;
                Logger.RedisLog.Error(errstr, ex);
                throw ex;
            }
        }

        /// <summary>
        /// 查找Keys
        /// </summary>
        /// <param name="keysPattern">查询表达式</param>
        /// <returns>返回Key列表</returns>
        public static IList<string> SearchKeyList(string keysPattern)
        {
            RedisKey rk = keysPattern.RedisProtobuf();
            return rk.Keys();
        }

        #endregion
    }
}
