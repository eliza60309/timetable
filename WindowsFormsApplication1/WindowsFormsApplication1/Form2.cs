using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
	public partial class Form2 : Form
	{
		public Form2(Form1 parent)
		{
			InitializeComponent();
			this.parent = parent;
		}

		public Form1 parent;
		private void textBox1_TextChanged(object sender, EventArgs e)
		{
			if (textBox1.Text.Length != 0 && textBox1.Text[textBox1.Text.Length - 1] == '\n')
			{
				List<string> list_s = new List<string>();
				string s = "";
				for (int i = 0, j = 0; ; j++)
				{
					if (j == textBox1.Text.Length - 2)
					{
						if (i != 0)
						{
							list_s.Add(s);
							s = "";
						}
						break;
					}
					else if (textBox1.Text[j] == ' ')
					{
						if (i != 0)
						{
							list_s.Add(s);
							s = "";
						}
						i = 0;
					}
					else
					{
						s = string.Concat(s, textBox1.Text[j] + "");
						i++;
					}

				}
				textBox1.ResetText();
				textBox1.Text = "";
				if (list_s.Count == 0) return;
				Form3 form3 = new Form3();
				if (list_s[0] == "set" || list_s[0] == "add")
				{
					int k = parent.set(list_s);
					if (k == 0) form3.show_msg("Success add " + list_s[1]);
					else if (k == 1) form3.show_msg("TUPLE");
					else if (k == 2) form3.show_msg("TIME " + list_s[2] + " ILLEGAL");
					else if (k == 3) form3.show_msg("TIME " + list_s[2] + " CONFLICT");
				}
				else if(list_s[0] == "delete")
				{
					int k = parent.delete(list_s[1]);
					if (k == 0) form3.show_msg("Success delete " + list_s[1]);
					else if(k == 1) form3.show_msg("Course not found " + list_s[1]);
				}
				else if(list_s[0] == "echo")
				{
					string msg = "";
					for(int i = 1; i < list_s.Count; i++)
					{
						msg = msg + " " + list_s[i];
					}
					form3.show_msg(msg);
				}
				/*else if(list_s[0] == "export")
				{
					////////////////////////////////////////////////////////////not finished
					int k = 3;
					if(list_s.Count() == 1)k = parent.export();
					else if (list_s.Count() == 2) k = parent.export(list_s[1]);
					if(k == 0) form3.show_msg("Success export ");
					else if(k == 1) form3.show_msg("Directory Not Found" + list_s[1]);
				}*/
				else form3.show_msg("I DONT KNOW");
				/*if (textBox1.Text.StartsWith("set")) ;//"set";
				else if (textBox1.Text.StartsWith("delete")) ;//"delete";
				else if (textBox1.Text.StartsWith("import")) ;//"import";
				else if (textBox1.Text.StartsWith("export")) ;//"Export";
				else;// button1.Text = "??";
				;// label1.Text = textBox1.Text;
				textBox1.Text = "";*/
			}
		}
		public void focus()
		{
			textBox1.Focus();
		}
	}
}
