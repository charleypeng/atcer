// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.HRCenter.Domains;

namespace ATCer.HRCenter
{
    /// <summary>
    /// 字典静态类
    /// </summary>
    public static class StaticDict
    {
        /// <summary>
        /// 扇区名字典
        /// </summary>
        /// <returns></returns>
        public static Dictionary<int, string> SectorNameDict()
        {
            IRepository<Sector> sectorRepo = Db.GetRepository<Sector>();
            if (sectorRepo == null)
                throw new Exception("repo is not registered");

            var sectors = sectorRepo.AsDefaultQuaryable().ToListAsync().Result;
            if(sectors == null)
                return new Dictionary<int, string>();

            var sectorDict = new Dictionary<int, string>();
            foreach (var item in sectors)
            {
                sectorDict.Add(item.Id, item.Name);
            }

            return sectorDict;
        }
         
    }
}
