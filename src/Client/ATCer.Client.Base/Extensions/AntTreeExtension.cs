// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using AntDesign;
using System.Collections.Generic;

namespace ATCer.Client.Base
{
    public static class AntTreeExtension
    {
        
        /// <summary>
        /// 获取所有前辈，node
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static List<TreeNode<T>> GetPredecessors<T>(this TreeNode<T> node)
        {
            List<TreeNode<T>> parents = new List<TreeNode<T>>();
            if (node.ParentNode != null)
            {
                parents.Add(node.ParentNode);
                parents.AddRange(node.ParentNode.GetPredecessors());
            }
            return parents;
        }

        /// <summary>
        /// 获取下一级所有node
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static List<TreeNode<T>> ChildNodes<T>(this TreeNode<T> node)
        {
            TreeNode<T> childNode = node.FindFirstOrDefaultNode(x => x.ParentNode.Key.Equals(node.Key), true);
            if (childNode == null)
            {
                return new List<TreeNode<T>>();
            }
            return childNode.GetParentNodes();
        }
    }
}
