using System;

namespace ATCer
{
    /// <summary>
    /// ����״̬
    /// </summary>
    public enum NetworkStatus
    {
        /// <summary>
        /// �����ѶϿ�
        /// </summary>
        Disconnected,
        /// <summary>
        /// ����Ͽ���
        /// </summary>
        Disconnecting,
        /// <summary>
        /// ����������
        /// </summary>
        Connected,
        /// <summary>
        /// ����������
        /// </summary>
        Connecting,
        /// <summary>
        /// �����޷�����
        /// </summary>
        Unreachable,
        /// <summary>
        /// �������ش���
        /// </summary>
        FatalError,
    }
    /// <summary>
    /// ����״̬��Ϣ����
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="status"></param>
    public delegate void NetworkStatusChangedEventHandler(object sender, NetworkStatus status);
}