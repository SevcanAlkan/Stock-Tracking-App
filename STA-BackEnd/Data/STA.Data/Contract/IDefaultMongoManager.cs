using STA.Core.MongoDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace STA.Data.Contract
{
    public interface IDefaultMongoManager<T> where T : BaseBson, new()
    {
        List<T> Get();
        T Get(string id);
        T Create(T rec);
        void Update(string id, T recIn);
        void Remove(T recIn);
        void Remove(string id);
    }
}
