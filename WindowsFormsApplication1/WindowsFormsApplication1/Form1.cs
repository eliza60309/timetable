using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace WindowsFormsApplication1
{
	public partial class Form1 : Form
	{
		DataGridViewRowCollection rows;
		public List<Course> Courses;
		Dictionary<char, int> map;
		public Form1()
		{
			InitializeComponent();
			foreach (DataGridViewColumn column in dataGridView1.Columns)
			{
				column.SortMode = DataGridViewColumnSortMode.NotSortable;
			}
			Form2 form2 = new Form2(this);
			form2.Show();
			map = new Dictionary<char, int>();
			map['A'] = 0;
			map['B'] = 1;
			map['C'] = 2;
			map['D'] = 3;
			map['X'] = 4;
			map['E'] = 5;
			map['F'] = 6;
			map['G'] = 7;
			map['H'] = 8;
			map['Y'] = 9;
			map['I'] = 10;
			map['J'] = 11;
			map['K'] = 12;
			map['L'] = 13;
			map['a'] = 0;
			map['b'] = 1;
			map['c'] = 2;
			map['d'] = 3;
			map['x'] = 4;
			map['e'] = 5;
			map['f'] = 6;
			map['g'] = 7;
			map['h'] = 8;
			map['y'] = 9;
			map['i'] = 10;
			map['j'] = 11;
			map['k'] = 12;
			map['l'] = 13;
			rows = dataGridView1.Rows;
			rows.Add(new Object[] { "A" });
			rows.Add(new Object[] { "B" });
			rows.Add(new Object[] { "C" });
			rows.Add(new Object[] { "D" });
			rows.Add(new Object[] { "X" });
			rows.Add(new Object[] { "E" });
			rows.Add(new Object[] { "F" });
			rows.Add(new Object[] { "G" });
			rows.Add(new Object[] { "H" });
			rows.Add(new Object[] { "Y" });
			rows.Add(new Object[] { "I" });
			rows.Add(new Object[] { "J" });
			rows.Add(new Object[] { "K" });
			rows.Add(new Object[] { "L" });  
			for (int i = 0; i < dataGridView1.RowCount; i++)
			{
				for(int j = 0; j < dataGridView1.ColumnCount; j++)
				{
					if (i == 4 || i == 9) rows[i].Cells[j].Style.BackColor = Color.Green;
				}
			}
			Courses = new List<Course>();
			form2.focus();
		}
		private void button1_Click_1(object sender, EventArgs e)
		{
			
		}
		public class Course
		{
			public string Coursename;
			public string Time;
			public string Location;
			public Course()
			{
				Coursename = "";
				Time = "";
				Location = "";
			}
		}
		public int set(List<string> list_s)
		{
			//three tuples needed for this func
			//Aka COMMAND[0] & COURSENAME & TIME
			if (list_s.Count < 3) return 1;// MISSING TUPLES
			Course c = new Course();
			c.Coursename = list_s[1];
			c.Time = list_s[2];
			if(list_s.Count >= 3) c.Location = list_s[2];
			if(check_legal_time(c.Time) != 0)return 2;//ILLEGAL TIMES
			if (check_time_conflict(c.Time) != 0) return 3;//TIME CONFLICT
			Courses.Add(c);
			set_timetable(c.Coursename , c.Time, c.Location);
			return 0;
		}
		public int check_legal_time(string s)
		{
			for(int i = 0; i < s.Length; i++)
			{
				if ((s[i] < '1' || s[i] > '5' ) && (s[i] < 'A' || s[i] > 'L') && s[i] != 'X' && s[i] != 'Y' && (s[i] < 'a' || s[i] > 'l') && s[i] != 'x' && s[i] != 'y') return 1;
			}
			return 0;
		}
		public int check_time_conflict(string Time)
		{
			for (int i = 0; i < Time.Length; i++)
			{
				if (Time[i] >= '1' && Time[i] <= '5')
				{
					int n = Time[i] - '1';
					for (int j = i + 1; j < Time.Length; j++)
					{
						i = j;
						if (Time[i] >= '1' && Time[i] <= '5') break;
						else if (rows[map[Time[i]]].Cells[n + 1].Style.BackColor == Color.Yellow) return 1;
					}
					i--;
				}
			}
			return 0;
		}
		public int set_timetable(string Coursename, string Time, string Location)
		{
			for(int i = 0; i < Time.Length; i++)
			{
				if (Time[i] >= '1' && Time[i] <= '5')
				{
					int n =	Time[i] - '1';
					for (int j = i + 1; j < Time.Length; j++)
					{
						i = j;
						if (Time[i] >= '1' && Time[i] <= '5') break;
						else set_element(map[Time[i]], n, Coursename, Location);
					}
					i--;
				}
				
			}
			return 0;
		}
		public int set_element(int r, int c, string Coursename, string Location)
		{
			//Debug.WriteLine(r + " " + c);
			rows[r].Cells[c + 1].Style.BackColor = Color.Yellow;
			rows[r].Cells[c + 1].Value = Coursename;
			return 0;
		}
		public int delete(string s)
		{
			foreach(var course in Courses)
			{
				if(s == course.Coursename)
				{
					delete_timetable(course.Coursename, course.Time, course.Location);
					Courses.Remove(course);
					return 0;
				}
			}
			return 1;
		}
		public int delete_timetable(string Coursename, string Time, string Location)
		{
			for (int i = 0; i < Time.Length; i++)
			{
				if (Time[i] >= '1' && Time[i] <= '5')
				{
					int n = Time[i] - '1';
					for (int j = i + 1; j < Time.Length; j++)
					{
						i = j;
						if (Time[i] >= '1' && Time[i] <= '5') break;
						else delete_element(map[Time[i]], n, Coursename, Location);
					}
					i--;
				}

			}
			return 0;
		}
		public int delete_element(int r, int c, string Coursename, string Location)
		{
			//Debug.WriteLine(r + " " + c);
			rows[r].Cells[c + 1].Style.BackColor = SystemColors.Control;
			rows[r].Cells[c + 1].Value = "";
			return 0;
		}
		private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{

		}
	}
}
