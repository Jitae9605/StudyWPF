using System;
using System.Collections.Generic;

class Solution1
{
	public int solution(int[,] p)
	{
		int answer = 0;

		for (int i = 0; i < p.GetLength(0); i++)
		{
			for (int j = 0; j < p.GetLength(0); j++)
			{
				if (p[i, 0] == p[j, 1])
				{
					if (p[i, 1] == p[j, 0])
					{
						answer++;
					}
				}
			}
		}

		return answer / 2;
	}
}

public class Solution
{
	public int solution(int[] estimates, int k)
	{
		int answer = 0;
		int tmp = 0;

		for (int i = 0; i <= estimates.Length - k; i++)
		{
			for (int j = i; j < i + k; j++)
			{
				tmp += estimates[j];
			}
			if (tmp > answer) answer = tmp;
			tmp = 0;
		}

		return answer;
	}
}

public class Solution2
{
	public string solution(string encrypted_text, string key, int rotation)
	{
		string answer = "";
		int len = encrypted_text.Length;
		int tmp;
		char tmpch;
		string tmpstr;

		for (int i = 0; i < rotation; i++)
		{
			tmpch = encrypted_text[0];
			encrypted_text = encrypted_text.Substring(1);
			encrypted_text += tmpch;
		}

		for (int i = 0; i < len; i++)
		{
			tmp = (int)key[i] - 96;
			tmpch = (char)((int)encrypted_text[i] - tmp);
			if (tmpch < 97) tmpch = (char)((int)tmpch + 26);
			answer += tmpch;
		}

		return answer;
	}
}
public class Solution3
{
	List<string> list = new List<string>();

	public void makeStr(string str, string str2)
	{
		if (str.Length == 0)
		{
			if (!list.Contains(str2))
			{
				list.Add(str2);
			}
		}
		for (int i = 0; i < str.Length; i++)
		{
			string tmp = str.Substring(0, i) + str.Substring(i + 1);
			makeStr(tmp, str2 + str[i]);
		}
	}

	public int solution(int n, int m)
	{
		int sum = n + m;
		int fac = 1;
		string str = "";

		for (int i = 1; i <= sum; i++)
		{
			fac *= i;
		}

		for (int i = 0; i < n; i++)
		{
			str += "(";
		}

		for (int i = 0; i < m; i++)
		{
			str += ")";
		}

		makeStr(str, "");

		return list.Count;
	}
}