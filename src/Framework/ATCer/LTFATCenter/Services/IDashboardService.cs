using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using ATCer.LTFATCenter.Dtos;

namespace ATCer.LTFATCenter.Services
{
    /// <summary>
    /// �������
    /// </summary>
    public interface IDashboardService
    {
        /// <summary>
        /// SIDͳ��
        /// </summary>
        /// <returns></returns>
        Task<IList<object>> GetSIDStats();
    }
}