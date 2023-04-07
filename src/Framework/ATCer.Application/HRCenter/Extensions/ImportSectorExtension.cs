using System;
namespace ATCer.HRCenter.Extensions
{
    public static class ImportSectorExtension
    {
        public static IList<SectorDto> AdaptSector(this IEnumerable<ImportSectorDto> infoDtos)
        {
            if (infoDtos == null || infoDtos.Count() == 0)
                return default(IList<SectorDto>)!;

            var lst = new List<SectorDto>();
            foreach (var item in infoDtos)
            {
                var info = infoDtos.Adapt<SectorDto>();
                info.CreatedTime = DateTime.Now;

                lst.Add(info);
            }
            return lst;
        }
    }
}

