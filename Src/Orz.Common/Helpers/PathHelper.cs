using System.IO;

namespace Orz.Common.Helpers
{
    /// <summary>
    /// <see cref="Path"/>辅助类
    /// </summary>
    public static class PathHelper
    {
        /// <summary>
        /// 确保父目录存在
        /// </summary>
        /// <param name="path"></param>
        public static void MakeParentDirectoryExist(string path)
        {
            var parent = Directory.GetParent(path);
            if (parent.Exists == false)
            {
                MakeParentDirectoryExist(parent.FullName);
                Directory.CreateDirectory(parent.FullName);
            }
        }

        /// <summary>
        /// 确保目录存在
        /// </summary>
        /// <param name="directoryPath">目录路径</param>
        public static void MakeDirectoryExist(string directoryPath)
        {
            if (Directory.Exists(directoryPath) == false)
            {
                MakeParentDirectoryExist(directoryPath);
                Directory.CreateDirectory(directoryPath);
            }
        }
    }
}
