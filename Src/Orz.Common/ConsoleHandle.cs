using System;
using System.Collections.Generic;
using System.Linq;
using Orz.Common.Helpers;

namespace Orz.Common
{
	/// <summary>
	/// 控制台处理输入命令的类
	/// </summary>
	public class ConsoleHandle
	{
		private bool run;
		private string separator;
		private string lastExitCommand;
		private StringSplitOptions paramsOptions;
		private ConsoleHandleOptions handleOptions;
		private Dictionary<string, Action<string, string[]>> dict;

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="separator">命令与参数和参数与参数之间的分隔符</param>
		/// <param name="paramsOptions">参数分隔时是否去除空子字符串</param>
		/// <param name="handleOptions">命令和参数处理枚举</param>
		/// <param name="exitCommand">退出命令</param>
		/// <param name="exitHandle">退出处理函数，第一个是退出命令，第二个是参数数组</param>
		/// <exception cref="ArgumentNullException"><paramref name="separator"/>为null，或者<paramref name="exitCommand"/>为null或空白字符串</exception>
		public ConsoleHandle(string separator = " ", StringSplitOptions paramsOptions = StringSplitOptions.None, ConsoleHandleOptions handleOptions = ConsoleHandleOptions.CommandToLower, string exitCommand = "exit", Action<string, string[]> exitHandle = null)
		{
			ThrowHelper.CheckArgumentNullOrEmpty(separator, nameof(separator), $"{nameof(separator)}不能为null或空串");
			ThrowHelper.CheckArgumentNullOrWhiteSpace(exitCommand, nameof(exitCommand), $"{nameof(exitCommand)}不能为null或空白字符串");

			this.separator = separator;
			this.paramsOptions = paramsOptions;
			this.handleOptions = handleOptions;

			lastExitCommand = "";
			var stringComparer = IsCommandIgnoreCase() ? StringComparer.OrdinalIgnoreCase : StringComparer.Ordinal;
			dict = new Dictionary<string, Action<string, string[]>>(stringComparer);

			SetExitHandle(exitCommand, exitHandle);
		}

		/// <summary>
		/// 添加命令和对应的处理函数
		/// </summary>
		/// <param name="commmand">命令</param>
		/// <param name="handle">处理函数，第一个是命令，第二个是参数数组</param>
		/// <exception cref="ArgumentNullException"><paramref name="commmand"/>为null或空白字符串，或者<paramref name="handle"/>为null</exception>
		/// <returns></returns>
		public ConsoleHandle AddCommandHandle(string commmand, Action<string, string[]> handle)
		{
			ThrowHelper.CheckArgumentNullOrWhiteSpace(commmand, nameof(commmand), $"{nameof(commmand)}不能为null或空白字符串");
			ThrowHelper.CheckArgumentNull(handle, nameof(handle));

			dict[commmand.Trim()] = handle;
			return this;
		}

		/// <summary>
		/// 移除命令和对应的处理函数
		/// </summary>
		/// <param name="commmand">命令</param>
		/// <returns></returns>
		public ConsoleHandle RemoveCommandHandle(string commmand)
		{
			if (!string.IsNullOrWhiteSpace(commmand)) dict.Remove(commmand.Trim());
			return this;
		}

		/// <summary>
		/// 设置退出命令和退出处理函数
		/// </summary>
		/// <param name="exitCommand">退出命令</param>
		/// <param name="exitHandle">退出处理函数，第一个是退出命令，第二个是参数数组</param>
		/// <exception cref="ArgumentNullException"><paramref name="exitCommand"/>为null或空白字符串</exception>
		/// <returns></returns>
		public ConsoleHandle SetExitHandle(string exitCommand, Action<string, string[]> exitHandle = null)
		{
			ThrowHelper.CheckArgumentNullOrWhiteSpace(exitCommand, nameof(exitCommand), $"{nameof(exitCommand)}不能为null或空白字符串");

			dict.Remove(lastExitCommand);

			Action<string, string[]> action;
			if (exitHandle == null)
			{
				action = (command, param) =>
				{
					run = false;
				};
			}
			else
			{
				action = (command, param) =>
				{
					run = false;
					exitHandle(command, param);
				};
			}

			lastExitCommand = exitCommand.Trim();
			dict[lastExitCommand] = action;
			return this;
		}

		/// <summary>
		/// 开始处理命令
		/// </summary>
		public void Start()
		{
			run = true;
			while (run)
			{
				var line = Console.ReadLine().Trim();

				string commandStr;
				string paramsStr;
				int index = line.IndexOf(separator);
				if (index > -1)
				{
					commandStr = line.Substring(0, index).TrimEnd();
					paramsStr = line.Substring(index + separator.Length);
				}
				else
				{
					commandStr = line;
					paramsStr = string.Empty;
				}

				if (dict.TryGetValue(commandStr, out var handle))
				{
					string command = HandleCommand(commandStr);
					var param = NeedToHandleParams() ? paramsStr.Split(separator, paramsOptions).Select(t => HandleParams(t)).ToArray() : paramsStr.Split(separator, paramsOptions);
					handle(command, param);
				}
				else
				{
					Console.WriteLine($"命令:[{commandStr}]，找不到对应的处理函数");
				}
			}
		}

		/// <summary>
		/// 输出消息并开始处理命令
		/// </summary>
		/// <param name="message">消息</param>
		public void Start(string message)
		{
			Console.WriteLine(message);
			Start();
		}

		private bool IsCommandIgnoreCase()
		{
			return (handleOptions & ConsoleHandleOptions.CommandToLower) == ConsoleHandleOptions.CommandToLower || (handleOptions & ConsoleHandleOptions.CommandToUpper) == ConsoleHandleOptions.CommandToUpper;
		}

		private bool NeedToHandleParams()
		{
			return (handleOptions & ConsoleHandleOptions.ParamsToLower) == ConsoleHandleOptions.ParamsToLower || (handleOptions & ConsoleHandleOptions.ParamsToUpper) == ConsoleHandleOptions.ParamsToUpper;
		}

		private string HandleCommand(string commandStr)
		{
			if ((handleOptions & ConsoleHandleOptions.CommandToLower) == ConsoleHandleOptions.CommandToLower) return commandStr.ToLower();
			if ((handleOptions & ConsoleHandleOptions.CommandToUpper) == ConsoleHandleOptions.CommandToUpper) return commandStr.ToUpper();
			return commandStr;
		}

		private string HandleParams(string paramsStr)
		{
			if ((handleOptions & ConsoleHandleOptions.ParamsToLower) == ConsoleHandleOptions.ParamsToLower) return paramsStr.ToLower();
			if ((handleOptions & ConsoleHandleOptions.ParamsToUpper) == ConsoleHandleOptions.ParamsToUpper) return paramsStr.ToUpper();
			return paramsStr;
		}
	}

	/// <summary>
	/// 命令和参数处理枚举
	/// </summary>
	[Flags]
	public enum ConsoleHandleOptions
	{
		/// <summary>
		/// 没有任何处理
		/// </summary>
		None = 0x0000,
		/// <summary>
		/// 命令转小写
		/// </summary>
		CommandToLower = 0x0001,
		/// <summary>
		/// 命令转大写
		/// </summary>
		CommandToUpper = 0x0002,
		/// <summary>
		/// 参数转小写
		/// </summary>
		ParamsToLower = 0x0004,
		/// <summary>
		/// 参数转大写
		/// </summary>
		ParamsToUpper = 0x0008,
		/// <summary>
		/// 命令和参数转小写
		/// </summary>
		ToLower = CommandToLower | ParamsToLower,
		/// <summary>
		/// 命令和参数转大写
		/// </summary>
		ToUpper = CommandToUpper | ParamsToUpper,
	}
}
