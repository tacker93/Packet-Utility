using System;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Packet_Utility.Packet;

namespace Packet_Utility
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            comboBox1.SelectedIndexChanged +=
                (sender, e) => numericUpDown1.Enabled = comboBox1.SelectedItem.Equals("String");
        }

        #region Methods

        private string WritePacket(PacketWriter p, string text)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    p.WriteByte(byte.Parse(text));
                    break;
                case 1:
                    p.WriteShort(short.Parse(text));
                    break;
                case 2:
                    p.WriteInt(int.Parse(text));
                    break;
                case 3:
                    p.WriteLong(long.Parse(text));
                    break;
                case 4:
                    p.WriteString(text);
                    break;
                case 5:
                    p.WriteMapleString(text);
                    break;
                default:
                    return String.Format("Unhandled Type: [ {0} ] with Index: {1}", comboBox1.SelectedItem, comboBox1.SelectedIndex);
            }
            return p.ToString();
        }

        private string ReadPacket(PacketReader p)
        {
            string convert;

            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    convert = p.ReadByte().ToString(CultureInfo.InvariantCulture);
                    break;
                case 1:
                    convert = p.ReadShort().ToString(CultureInfo.InvariantCulture);
                    break;
                case 2:
                    convert = p.ReadInt().ToString(CultureInfo.InvariantCulture);
                    break;
                case 3:
                    convert = p.ReadLong().ToString(CultureInfo.InvariantCulture);
                    break;
                case 4:
                    convert = p.ReadString((int)numericUpDown1.Value);
                    break;
                case 5:
                    convert = p.ReadMapleString();
                    break;
                case 6:
                    convert = p.Length + " Byte(s)";
                    break;
                default:
                    convert = String.Format("Unhandled Type: [ {0} ] with Index: {1}", comboBox1.SelectedItem, comboBox1.SelectedIndex);
                    break;
            }
            return convert;
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (var p = new PacketReader(AbstractPacket.StringToByteArray(richTextBox1.Text)))
                    richTextBox2.Text = ReadPacket(p);
            }
            catch (Exception ex)
            {
                richTextBox2.Text = ex.Message;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                using (var p = new PacketWriter())
                    richTextBox2.Text = WritePacket(p, richTextBox1.Text);
            }
            catch (Exception ex)
            {
                richTextBox2.Text = ex.Message;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var s = new String(richTextBox3.Text.Where(Char.IsDigit).ToArray());
            richTextBox4.Text = String.Join(" ", s.SplitInGroups(2).ToArray());
        }
    }
}