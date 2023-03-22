// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

namespace ATCer.DataRecorder
{
    public class DataRecorderOptions
    {
        internal IList<IRecoerderExtension> Extensions { get; }

        public IEnumerable<RecorderOptions> Recorders { get; set; }

        public DataRecorderOptions()
        {
            Extensions = new List<IRecoerderExtension>();
            Recorders = new List<RecorderOptions>();
        }

        public void RegisterExtension(IRecoerderExtension extension)
        {
            if(extension == null)
            {
                throw new ArgumentNullException(nameof(extension));
            }

            Extensions.Add(extension);
        }
    }
}
