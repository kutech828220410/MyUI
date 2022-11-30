using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyUI;
using LadderConnection;

namespace LeadShineUI
{
    public partial class IOC1280_Basic : UserControl
    {
        private LowerMachine PLC;

        private bool flag_Init_OK = false;
        public static readonly int InputNum = 64;
        public static readonly int OutputNum = 64;

        private List<PLC_Button> List_PLC_Button_Input = new List<PLC_Button>();
        private List<NumWordTextBox> List_NumWordTextBox_Input = new List<NumWordTextBox>();
        private List<PLC_Button> List_PLC_Button_Output = new List<PLC_Button>();
        private List<NumWordTextBox> List_NumWordTextBox_Output = new List<NumWordTextBox>();
        private List<CheckBox> List_CheckBox_Output_PCUse = new List<CheckBox>();

        public IOC1280_Basic()
        {
            InitializeComponent();
        }

        private void List_PLC_Button_Input_Init()
        {
            List_PLC_Button_Input.Add(plC_Button_I01);
            List_PLC_Button_Input.Add(plC_Button_I02);
            List_PLC_Button_Input.Add(plC_Button_I03);
            List_PLC_Button_Input.Add(plC_Button_I04);
            List_PLC_Button_Input.Add(plC_Button_I05);
            List_PLC_Button_Input.Add(plC_Button_I06);
            List_PLC_Button_Input.Add(plC_Button_I07);
            List_PLC_Button_Input.Add(plC_Button_I08);
            List_PLC_Button_Input.Add(plC_Button_I09);
            List_PLC_Button_Input.Add(plC_Button_I10);
            List_PLC_Button_Input.Add(plC_Button_I11);
            List_PLC_Button_Input.Add(plC_Button_I12);
            List_PLC_Button_Input.Add(plC_Button_I13);
            List_PLC_Button_Input.Add(plC_Button_I14);
            List_PLC_Button_Input.Add(plC_Button_I15);
            List_PLC_Button_Input.Add(plC_Button_I16);
            List_PLC_Button_Input.Add(plC_Button_I17);
            List_PLC_Button_Input.Add(plC_Button_I18);
            List_PLC_Button_Input.Add(plC_Button_I19);
            List_PLC_Button_Input.Add(plC_Button_I20);
            List_PLC_Button_Input.Add(plC_Button_I21);
            List_PLC_Button_Input.Add(plC_Button_I22);
            List_PLC_Button_Input.Add(plC_Button_I23);
            List_PLC_Button_Input.Add(plC_Button_I24);
            List_PLC_Button_Input.Add(plC_Button_I25);
            List_PLC_Button_Input.Add(plC_Button_I26);
            List_PLC_Button_Input.Add(plC_Button_I27);
            List_PLC_Button_Input.Add(plC_Button_I28);
            List_PLC_Button_Input.Add(plC_Button_I29);
            List_PLC_Button_Input.Add(plC_Button_I30);
            List_PLC_Button_Input.Add(plC_Button_I31);
            List_PLC_Button_Input.Add(plC_Button_I32);
            List_PLC_Button_Input.Add(plC_Button_I33);
            List_PLC_Button_Input.Add(plC_Button_I34);
            List_PLC_Button_Input.Add(plC_Button_I35);
            List_PLC_Button_Input.Add(plC_Button_I36);
            List_PLC_Button_Input.Add(plC_Button_I37);
            List_PLC_Button_Input.Add(plC_Button_I38);
            List_PLC_Button_Input.Add(plC_Button_I39);
            List_PLC_Button_Input.Add(plC_Button_I40);
            List_PLC_Button_Input.Add(plC_Button_I41);
            List_PLC_Button_Input.Add(plC_Button_I42);
            List_PLC_Button_Input.Add(plC_Button_I43);
            List_PLC_Button_Input.Add(plC_Button_I44);
            List_PLC_Button_Input.Add(plC_Button_I45);
            List_PLC_Button_Input.Add(plC_Button_I46);
            List_PLC_Button_Input.Add(plC_Button_I47);
            List_PLC_Button_Input.Add(plC_Button_I48);
            List_PLC_Button_Input.Add(plC_Button_I49);
            List_PLC_Button_Input.Add(plC_Button_I50);
            List_PLC_Button_Input.Add(plC_Button_I51);
            List_PLC_Button_Input.Add(plC_Button_I52);
            List_PLC_Button_Input.Add(plC_Button_I53);
            List_PLC_Button_Input.Add(plC_Button_I54);
            List_PLC_Button_Input.Add(plC_Button_I55);
            List_PLC_Button_Input.Add(plC_Button_I56);
            List_PLC_Button_Input.Add(plC_Button_I57);
            List_PLC_Button_Input.Add(plC_Button_I58);
            List_PLC_Button_Input.Add(plC_Button_I59);
            List_PLC_Button_Input.Add(plC_Button_I60);
            List_PLC_Button_Input.Add(plC_Button_I61);
            List_PLC_Button_Input.Add(plC_Button_I62);
            List_PLC_Button_Input.Add(plC_Button_I63);
            List_PLC_Button_Input.Add(plC_Button_I64);
        }
        public bool Get_Input(int index)
        {
            return List_PLC_Button_Input[index].GetValue();
        }
        public void Set_Input(int index, bool value)
        {
            List_PLC_Button_Input[index].SetValue(value);
        }

        private void List_PLC_Button_Output_Init()
        {
            List_PLC_Button_Output.Add(plC_Button_O01);
            List_PLC_Button_Output.Add(plC_Button_O02);
            List_PLC_Button_Output.Add(plC_Button_O03);
            List_PLC_Button_Output.Add(plC_Button_O04);
            List_PLC_Button_Output.Add(plC_Button_O05);
            List_PLC_Button_Output.Add(plC_Button_O06);
            List_PLC_Button_Output.Add(plC_Button_O07);
            List_PLC_Button_Output.Add(plC_Button_O08);
            List_PLC_Button_Output.Add(plC_Button_O09);
            List_PLC_Button_Output.Add(plC_Button_O10);
            List_PLC_Button_Output.Add(plC_Button_O11);
            List_PLC_Button_Output.Add(plC_Button_O12);
            List_PLC_Button_Output.Add(plC_Button_O13);
            List_PLC_Button_Output.Add(plC_Button_O14);
            List_PLC_Button_Output.Add(plC_Button_O15);
            List_PLC_Button_Output.Add(plC_Button_O16);
            List_PLC_Button_Output.Add(plC_Button_O17);
            List_PLC_Button_Output.Add(plC_Button_O18);
            List_PLC_Button_Output.Add(plC_Button_O19);
            List_PLC_Button_Output.Add(plC_Button_O20);
            List_PLC_Button_Output.Add(plC_Button_O21);
            List_PLC_Button_Output.Add(plC_Button_O22);
            List_PLC_Button_Output.Add(plC_Button_O23);
            List_PLC_Button_Output.Add(plC_Button_O24);
            List_PLC_Button_Output.Add(plC_Button_O25);
            List_PLC_Button_Output.Add(plC_Button_O26);
            List_PLC_Button_Output.Add(plC_Button_O27);
            List_PLC_Button_Output.Add(plC_Button_O28);
            List_PLC_Button_Output.Add(plC_Button_O29);
            List_PLC_Button_Output.Add(plC_Button_O30);
            List_PLC_Button_Output.Add(plC_Button_O31);
            List_PLC_Button_Output.Add(plC_Button_O32);
            List_PLC_Button_Output.Add(plC_Button_O33);
            List_PLC_Button_Output.Add(plC_Button_O34);
            List_PLC_Button_Output.Add(plC_Button_O35);
            List_PLC_Button_Output.Add(plC_Button_O36);
            List_PLC_Button_Output.Add(plC_Button_O37);
            List_PLC_Button_Output.Add(plC_Button_O38);
            List_PLC_Button_Output.Add(plC_Button_O39);
            List_PLC_Button_Output.Add(plC_Button_O40);
            List_PLC_Button_Output.Add(plC_Button_O41);
            List_PLC_Button_Output.Add(plC_Button_O42);
            List_PLC_Button_Output.Add(plC_Button_O43);
            List_PLC_Button_Output.Add(plC_Button_O44);
            List_PLC_Button_Output.Add(plC_Button_O45);
            List_PLC_Button_Output.Add(plC_Button_O46);
            List_PLC_Button_Output.Add(plC_Button_O47);
            List_PLC_Button_Output.Add(plC_Button_O48);
            List_PLC_Button_Output.Add(plC_Button_O49);
            List_PLC_Button_Output.Add(plC_Button_O50);
            List_PLC_Button_Output.Add(plC_Button_O51);
            List_PLC_Button_Output.Add(plC_Button_O52);
            List_PLC_Button_Output.Add(plC_Button_O53);
            List_PLC_Button_Output.Add(plC_Button_O54);
            List_PLC_Button_Output.Add(plC_Button_O55);
            List_PLC_Button_Output.Add(plC_Button_O56);
            List_PLC_Button_Output.Add(plC_Button_O57);
            List_PLC_Button_Output.Add(plC_Button_O58);
            List_PLC_Button_Output.Add(plC_Button_O59);
            List_PLC_Button_Output.Add(plC_Button_O60);
            List_PLC_Button_Output.Add(plC_Button_O61);
            List_PLC_Button_Output.Add(plC_Button_O62);
            List_PLC_Button_Output.Add(plC_Button_O63);
            List_PLC_Button_Output.Add(plC_Button_O64);

        }
        public bool Get_Output(int index)
        {
            return List_PLC_Button_Output[index].GetValue();
        }
        public void Set_Output(int index, bool value)
        {
            List_PLC_Button_Output[index].SetValue(value);
        }

        public void List_CheckBox_Output_PCUse_Init()
        {
            List_CheckBox_Output_PCUse.Add(checkBox_O01_PCUse);
            List_CheckBox_Output_PCUse.Add(checkBox_O02_PCUse);
            List_CheckBox_Output_PCUse.Add(checkBox_O03_PCUse);
            List_CheckBox_Output_PCUse.Add(checkBox_O04_PCUse);
            List_CheckBox_Output_PCUse.Add(checkBox_O05_PCUse);
            List_CheckBox_Output_PCUse.Add(checkBox_O06_PCUse);
            List_CheckBox_Output_PCUse.Add(checkBox_O07_PCUse);
            List_CheckBox_Output_PCUse.Add(checkBox_O08_PCUse);
            List_CheckBox_Output_PCUse.Add(checkBox_O09_PCUse);
            List_CheckBox_Output_PCUse.Add(checkBox_O10_PCUse);
            List_CheckBox_Output_PCUse.Add(checkBox_O11_PCUse);
            List_CheckBox_Output_PCUse.Add(checkBox_O12_PCUse);
            List_CheckBox_Output_PCUse.Add(checkBox_O13_PCUse);
            List_CheckBox_Output_PCUse.Add(checkBox_O14_PCUse);
            List_CheckBox_Output_PCUse.Add(checkBox_O15_PCUse);
            List_CheckBox_Output_PCUse.Add(checkBox_O16_PCUse);
            List_CheckBox_Output_PCUse.Add(checkBox_O17_PCUse);
            List_CheckBox_Output_PCUse.Add(checkBox_O18_PCUse);
            List_CheckBox_Output_PCUse.Add(checkBox_O19_PCUse);
            List_CheckBox_Output_PCUse.Add(checkBox_O20_PCUse);
            List_CheckBox_Output_PCUse.Add(checkBox_O21_PCUse);
            List_CheckBox_Output_PCUse.Add(checkBox_O22_PCUse);
            List_CheckBox_Output_PCUse.Add(checkBox_O23_PCUse);
            List_CheckBox_Output_PCUse.Add(checkBox_O24_PCUse);
            List_CheckBox_Output_PCUse.Add(checkBox_O25_PCUse);
            List_CheckBox_Output_PCUse.Add(checkBox_O26_PCUse);
            List_CheckBox_Output_PCUse.Add(checkBox_O27_PCUse);
            List_CheckBox_Output_PCUse.Add(checkBox_O28_PCUse);
            List_CheckBox_Output_PCUse.Add(checkBox_O29_PCUse);
            List_CheckBox_Output_PCUse.Add(checkBox_O30_PCUse);
            List_CheckBox_Output_PCUse.Add(checkBox_O31_PCUse);
            List_CheckBox_Output_PCUse.Add(checkBox_O32_PCUse);
        }
        public void Set_Output_PCUse(int index, bool value)
        {
            this.Invoke(new Action(delegate { this.List_CheckBox_Output_PCUse[index].Checked = value; }));
        }
        public bool Get_Output_PCUse(int index)
        {
            return this.List_CheckBox_Output_PCUse[index].Checked;
        }

        private void List_NumWordTextBox_Input_Init()
        {
            List_NumWordTextBox_Input.Add(numWordTextBox_I01);
            List_NumWordTextBox_Input.Add(numWordTextBox_I02);
            List_NumWordTextBox_Input.Add(numWordTextBox_I03);
            List_NumWordTextBox_Input.Add(numWordTextBox_I04);
            List_NumWordTextBox_Input.Add(numWordTextBox_I05);
            List_NumWordTextBox_Input.Add(numWordTextBox_I06);
            List_NumWordTextBox_Input.Add(numWordTextBox_I07);
            List_NumWordTextBox_Input.Add(numWordTextBox_I08);
            List_NumWordTextBox_Input.Add(numWordTextBox_I09);
            List_NumWordTextBox_Input.Add(numWordTextBox_I10);
            List_NumWordTextBox_Input.Add(numWordTextBox_I11);
            List_NumWordTextBox_Input.Add(numWordTextBox_I12);
            List_NumWordTextBox_Input.Add(numWordTextBox_I13);
            List_NumWordTextBox_Input.Add(numWordTextBox_I14);
            List_NumWordTextBox_Input.Add(numWordTextBox_I15);
            List_NumWordTextBox_Input.Add(numWordTextBox_I16);
            List_NumWordTextBox_Input.Add(numWordTextBox_I17);
            List_NumWordTextBox_Input.Add(numWordTextBox_I18);
            List_NumWordTextBox_Input.Add(numWordTextBox_I19);
            List_NumWordTextBox_Input.Add(numWordTextBox_I20);
            List_NumWordTextBox_Input.Add(numWordTextBox_I21);
            List_NumWordTextBox_Input.Add(numWordTextBox_I22);
            List_NumWordTextBox_Input.Add(numWordTextBox_I23);
            List_NumWordTextBox_Input.Add(numWordTextBox_I24);
            List_NumWordTextBox_Input.Add(numWordTextBox_I25);
            List_NumWordTextBox_Input.Add(numWordTextBox_I26);
            List_NumWordTextBox_Input.Add(numWordTextBox_I27);
            List_NumWordTextBox_Input.Add(numWordTextBox_I28);
            List_NumWordTextBox_Input.Add(numWordTextBox_I29);
            List_NumWordTextBox_Input.Add(numWordTextBox_I30);
            List_NumWordTextBox_Input.Add(numWordTextBox_I31);
            List_NumWordTextBox_Input.Add(numWordTextBox_I32);
            List_NumWordTextBox_Input.Add(numWordTextBox_I33);
            List_NumWordTextBox_Input.Add(numWordTextBox_I34);
            List_NumWordTextBox_Input.Add(numWordTextBox_I35);
            List_NumWordTextBox_Input.Add(numWordTextBox_I36);
            List_NumWordTextBox_Input.Add(numWordTextBox_I37);
            List_NumWordTextBox_Input.Add(numWordTextBox_I38);
            List_NumWordTextBox_Input.Add(numWordTextBox_I39);
            List_NumWordTextBox_Input.Add(numWordTextBox_I40);
            List_NumWordTextBox_Input.Add(numWordTextBox_I41);
            List_NumWordTextBox_Input.Add(numWordTextBox_I42);
            List_NumWordTextBox_Input.Add(numWordTextBox_I43);
            List_NumWordTextBox_Input.Add(numWordTextBox_I44);
            List_NumWordTextBox_Input.Add(numWordTextBox_I45);
            List_NumWordTextBox_Input.Add(numWordTextBox_I46);
            List_NumWordTextBox_Input.Add(numWordTextBox_I47);
            List_NumWordTextBox_Input.Add(numWordTextBox_I48);
            List_NumWordTextBox_Input.Add(numWordTextBox_I49);
            List_NumWordTextBox_Input.Add(numWordTextBox_I50);
            List_NumWordTextBox_Input.Add(numWordTextBox_I51);
            List_NumWordTextBox_Input.Add(numWordTextBox_I52);
            List_NumWordTextBox_Input.Add(numWordTextBox_I53);
            List_NumWordTextBox_Input.Add(numWordTextBox_I54);
            List_NumWordTextBox_Input.Add(numWordTextBox_I55);
            List_NumWordTextBox_Input.Add(numWordTextBox_I56);
            List_NumWordTextBox_Input.Add(numWordTextBox_I57);
            List_NumWordTextBox_Input.Add(numWordTextBox_I58);
            List_NumWordTextBox_Input.Add(numWordTextBox_I59);
            List_NumWordTextBox_Input.Add(numWordTextBox_I60);
            List_NumWordTextBox_Input.Add(numWordTextBox_I61);
            List_NumWordTextBox_Input.Add(numWordTextBox_I62);
            List_NumWordTextBox_Input.Add(numWordTextBox_I63);
            List_NumWordTextBox_Input.Add(numWordTextBox_I64);
        }
        public void Set_Input_Adress(int index, string Adress)
        {
            if (LadderProperty.DEVICE.TestDevice(Adress))
            {
                string temp = Adress.Remove(1);
                if (temp == "X")
                {
                    this.Invoke(new Action(delegate { List_NumWordTextBox_Input[index].Text = Adress; }));
                }
            }
        }
        public string Get_Input_Adress(int index)
        {
            return List_NumWordTextBox_Input[index].Text;
        }

        private void List_NumWordTextBox_Output_Init()
        {
            List_NumWordTextBox_Output.Add(numWordTextBox_O01);
            List_NumWordTextBox_Output.Add(numWordTextBox_O02);
            List_NumWordTextBox_Output.Add(numWordTextBox_O03);
            List_NumWordTextBox_Output.Add(numWordTextBox_O04);
            List_NumWordTextBox_Output.Add(numWordTextBox_O05);
            List_NumWordTextBox_Output.Add(numWordTextBox_O06);
            List_NumWordTextBox_Output.Add(numWordTextBox_O07);
            List_NumWordTextBox_Output.Add(numWordTextBox_O08);
            List_NumWordTextBox_Output.Add(numWordTextBox_O09);
            List_NumWordTextBox_Output.Add(numWordTextBox_O10);
            List_NumWordTextBox_Output.Add(numWordTextBox_O11);
            List_NumWordTextBox_Output.Add(numWordTextBox_O12);
            List_NumWordTextBox_Output.Add(numWordTextBox_O13);
            List_NumWordTextBox_Output.Add(numWordTextBox_O14);
            List_NumWordTextBox_Output.Add(numWordTextBox_O15);
            List_NumWordTextBox_Output.Add(numWordTextBox_O16);
            List_NumWordTextBox_Output.Add(numWordTextBox_O17);
            List_NumWordTextBox_Output.Add(numWordTextBox_O18);
            List_NumWordTextBox_Output.Add(numWordTextBox_O19);
            List_NumWordTextBox_Output.Add(numWordTextBox_O20);
            List_NumWordTextBox_Output.Add(numWordTextBox_O21);
            List_NumWordTextBox_Output.Add(numWordTextBox_O22);
            List_NumWordTextBox_Output.Add(numWordTextBox_O23);
            List_NumWordTextBox_Output.Add(numWordTextBox_O24);
            List_NumWordTextBox_Output.Add(numWordTextBox_O25);
            List_NumWordTextBox_Output.Add(numWordTextBox_O26);
            List_NumWordTextBox_Output.Add(numWordTextBox_O27);
            List_NumWordTextBox_Output.Add(numWordTextBox_O28);
            List_NumWordTextBox_Output.Add(numWordTextBox_O29);
            List_NumWordTextBox_Output.Add(numWordTextBox_O30);
            List_NumWordTextBox_Output.Add(numWordTextBox_O31);
            List_NumWordTextBox_Output.Add(numWordTextBox_O32);
            List_NumWordTextBox_Output.Add(numWordTextBox_O33);
            List_NumWordTextBox_Output.Add(numWordTextBox_O34);
            List_NumWordTextBox_Output.Add(numWordTextBox_O35);
            List_NumWordTextBox_Output.Add(numWordTextBox_O36);
            List_NumWordTextBox_Output.Add(numWordTextBox_O37);
            List_NumWordTextBox_Output.Add(numWordTextBox_O38);
            List_NumWordTextBox_Output.Add(numWordTextBox_O39);
            List_NumWordTextBox_Output.Add(numWordTextBox_O40);
            List_NumWordTextBox_Output.Add(numWordTextBox_O41);
            List_NumWordTextBox_Output.Add(numWordTextBox_O42);
            List_NumWordTextBox_Output.Add(numWordTextBox_O43);
            List_NumWordTextBox_Output.Add(numWordTextBox_O44);
            List_NumWordTextBox_Output.Add(numWordTextBox_O45);
            List_NumWordTextBox_Output.Add(numWordTextBox_O46);
            List_NumWordTextBox_Output.Add(numWordTextBox_O47);
            List_NumWordTextBox_Output.Add(numWordTextBox_O48);
            List_NumWordTextBox_Output.Add(numWordTextBox_O49);
            List_NumWordTextBox_Output.Add(numWordTextBox_O50);
            List_NumWordTextBox_Output.Add(numWordTextBox_O51);
            List_NumWordTextBox_Output.Add(numWordTextBox_O52);
            List_NumWordTextBox_Output.Add(numWordTextBox_O53);
            List_NumWordTextBox_Output.Add(numWordTextBox_O54);
            List_NumWordTextBox_Output.Add(numWordTextBox_O55);
            List_NumWordTextBox_Output.Add(numWordTextBox_O56);
            List_NumWordTextBox_Output.Add(numWordTextBox_O57);
            List_NumWordTextBox_Output.Add(numWordTextBox_O58);
            List_NumWordTextBox_Output.Add(numWordTextBox_O59);
            List_NumWordTextBox_Output.Add(numWordTextBox_O60);
            List_NumWordTextBox_Output.Add(numWordTextBox_O61);
            List_NumWordTextBox_Output.Add(numWordTextBox_O62);
            List_NumWordTextBox_Output.Add(numWordTextBox_O63);
            List_NumWordTextBox_Output.Add(numWordTextBox_O64);
        }
        public void Set_Output_Adress(int index, string Adress)
        {
            if (LadderProperty.DEVICE.TestDevice(Adress))
            {
                string temp = Adress.Remove(1);
                if (temp == "X")
                {
                    this.Invoke(new Action(delegate { List_NumWordTextBox_Output[index].Text = Adress; }));
                }
            }
        }
        public string Get_Output_Adress(int index)
        {
            return List_NumWordTextBox_Output[index].Text;
        }


        public void Init()
        {
            List_PLC_Button_Input_Init();
            List_PLC_Button_Output_Init();
            List_CheckBox_Output_PCUse_Init();
            List_NumWordTextBox_Input_Init();
            List_NumWordTextBox_Output_Init();
            flag_Init_OK = true;
        }

        public void SetPLC(LowerMachine PLC)
        {
            this.PLC = PLC;
        }
        public void RefreshUI()
        {
            foreach (PLC_Button pLC_Button in List_PLC_Button_Input)
            {
                pLC_Button.Run(this.PLC);
            }
            foreach (PLC_Button pLC_Button in List_PLC_Button_Output)
            {
                pLC_Button.Run(this.PLC);
            }         
        }
        public void Set_UI_Enable(bool enable)
        {
            this.Invoke(new Action(delegate
            {
                foreach (NumWordTextBox numWordTextBox in List_NumWordTextBox_Input)
                {
                    numWordTextBox.Enabled = enable;

                    if (!(LadderProperty.DEVICE.TestDevice(numWordTextBox.Text) && (numWordTextBox.Text.Remove(1) == "X")))
                    {
                        numWordTextBox.Text = "";
                    }
                }
                foreach (NumWordTextBox numWordTextBox in List_NumWordTextBox_Output)
                {
                    numWordTextBox.Enabled = enable;

                    if (!(LadderProperty.DEVICE.TestDevice(numWordTextBox.Text) && (numWordTextBox.Text.Remove(1) == "Y")))
                    {
                        numWordTextBox.Text = "";
                    }
                }
              
            }));
        }

        [Serializable]
        public class SaveClass
        {
            public List<string> Input_Adress = new List<string>();
            public List<string> Output_Adress = new List<string>();
            public List<bool> Output_PCUse = new List<bool>();
        }
        public SaveClass GetSaveObject()
        {
            SaveClass saveClass = new SaveClass();
         
            foreach (NumWordTextBox numWordTextBox in List_NumWordTextBox_Input)
            {
                saveClass.Input_Adress.Add(numWordTextBox.Text);
            }
            foreach (NumWordTextBox numWordTextBox in List_NumWordTextBox_Output)
            {
                saveClass.Output_Adress.Add(numWordTextBox.Text);
            }
            foreach (CheckBox checkBox in List_CheckBox_Output_PCUse)
            {
                saveClass.Output_PCUse.Add(checkBox.Checked);
            }
            return saveClass;
        }
        public void LoadObject(SaveClass saveClass)
        {
            this.Invoke(new Action(delegate
            {
                
                for (int i = 0; i < List_NumWordTextBox_Input.Count; i++)
                {
                    if (saveClass.Input_Adress != null) if (i < saveClass.Input_Adress.Count) List_NumWordTextBox_Input[i].Text = saveClass.Input_Adress[i];
                }
                for (int i = 0; i < List_NumWordTextBox_Output.Count; i++)
                {
                    if (saveClass.Output_Adress != null) if (i < saveClass.Output_Adress.Count) List_NumWordTextBox_Output[i].Text = saveClass.Output_Adress[i];
                }
                for (int i = 0; i < List_CheckBox_Output_PCUse.Count; i++)
                {
                    if (saveClass.Output_PCUse != null) if (i < saveClass.Output_PCUse.Count) List_CheckBox_Output_PCUse[i].Checked = saveClass.Output_PCUse[i];
                }
            }));

        }
    }
}
