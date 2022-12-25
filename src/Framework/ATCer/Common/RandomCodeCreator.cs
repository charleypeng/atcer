﻿// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Enums;
using System;
using System.Text;

namespace ATCer.Common
{
    /// <summary>
    /// 随机码生成器
    /// </summary>
    public class RandomCodeCreator
    {
        static readonly char[] Num = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        static readonly char[] Character = new char[] {
            'a','b','c','d','e','f','g','h','i','j','k','m','n','p','q','r','s','t','u','v','w','x','y','z',
            'A','B','C','D','E','F','G','H','J','K','L','M','N','P','Q','R','S','T','U','V','W','X','Y','Z'
        };

        static readonly char[] NumAndCharacter = new char[] {
            '0','1','2','3','4','5','6','7','8','9',
            'a','b','c','d','e','f','g','h','i','j','k','m','n','p','q','r','s','t','u','v','w','x','y','z',
            'A','B','C','D','E','F','G','H','J','K','L','M','N','P','Q','R','S','T','U','V','W','X','Y','Z'
        };
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string Create(CodeCharacterTypeEnum type, int count)
        {
            string code = string.Empty;
            switch (type)
            {
                case CodeCharacterTypeEnum.Character:
                    code = RandomCodeCreator.CreatRandomChar(count);
                    break;
                case CodeCharacterTypeEnum.Number:
                    code = RandomCodeCreator.CreatRandomNum(count);
                    break;
                case CodeCharacterTypeEnum.NumberAndCharacter:
                    code = RandomCodeCreator.CreatRandomNumAndChar(count);
                    break;
            }
            return code;
        }
        /// <summary>  
        /// 生成指定位数的字符和数字随机数  
        /// </summary>  
        /// <param name="count">随机数的位数</param>  
        /// <returns>随机字符串</returns>  
        public static string CreatRandomNumAndChar(int count = 6)
        {
            return InnerCreatRandomCode(NumAndCharacter, count);
        }

        /// <summary>  
        /// 生成指定位数的字符随机数  
        /// </summary>  
        /// <param name="count">随机数的位数</param>  
        /// <returns>随机字符串</returns>  
        public static string CreatRandomChar(int count = 6)
        {
            return InnerCreatRandomCode(Character, count);
        }

        /// <summary>  
        /// 生成指定位数的数字随机数  
        /// </summary>  
        /// <param name="count">随机数的位数</param>  
        /// <returns>随机数字符串</returns>  
        public static string CreatRandomNum(int count = 4)
        {
            return InnerCreatRandomCode(Num, count);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseCharArr"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        private static string InnerCreatRandomCode(char[] baseCharArr, int count)
        {
            StringBuilder code = new StringBuilder();
            //记录上次随机数值，尽量避免生产几个一样的随机数  
            int temp = -1;

            Random rand = new Random();
            for (int i = 0; i < count; i++)
            {
                int t = rand.Next(baseCharArr.Length);
                //避免生产一样的随机数
                while (temp == t)
                {
                    t = rand.Next(baseCharArr.Length);
                }
                //把本次产生的随机数记录起来  
                temp = t;
                code.Append(baseCharArr[t]);
            }

            return code.ToString();
        }

    }
}
