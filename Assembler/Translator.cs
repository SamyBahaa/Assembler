using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assembler
{
    class Translator
    {
        public List<string> Binaries;
        Read fileRead;
        Write writeFile;

        public Translator()
        {
            Binaries = new List<string>(4096);
            for (int i = 0; i < 4096; i++)
                Binaries.Add("0000000000000000");
            writeFile = new Write();
        }

        public void Clear()
        {
            for (int i = 0; i < 4096; i++)
            {
                Binaries[i] = "0000000000000000";
            }
        }

        public int Convert(string text, ref string msgError)
        {

            Clear();
            fileRead = new Read(text);
            UInt16 address = 0;
            int i = 0;
            try
            {
                for (i = 0; i < fileRead.clearFile.Count; i++)
                {
                    string Code     = "";
                    string subCode  = "";
                    char[] dataChar ;
                    string menmonic = fileRead.clearFile[i];
                    menmonic = menmonic.Trim();
                    if (menmonic == "")
                        continue;
                    List<string> datas = menmonic.Split(' ').ToList();
                    if (datas[0] == "#")
                    { continue; }
                    dataChar = datas[0].ToCharArray();
                    if (dataChar[0].ToString() == "#")
                    { continue; }

                    for (int j = 0; j < datas.Count; j++)
                    {
                         dataChar = datas[j].ToCharArray();
                        if ( dataChar.Length != 0)
                        {
                            if (dataChar[0].ToString() == "#")
                            {

                                int counter = datas.Count;
                                for (int k = counter - 1; k >= j; k--)
                                {
                                    datas.RemoveAt(k);
                                }
                                break;
                            }
                        }
                        else if (datas[j] == "")
                        {
                            datas.RemoveAt(j);
                            j -= j;
                        }
                    }

                    if (datas.Count == 2)
                    {
                        datas.AddRange(datas[1].Split(',').ToList());
                        datas.RemoveAt(1);
                    }
                    else if (datas.Count == 3)
                    {
                        datas[1] = datas[1].Split(',')[0];
                    }
                    else if (datas.Count == 4 && datas[2] == ",")
                    {
                        datas.RemoveAt(2);
                    }

                    string opcode = datas[0];

                    switch (opcode)
                    {
                        case "":
                            {
                                break;
                            }
                        case ".org":
                            {
                                if (datas.Count != 2)
                                {
                                    msgError = "Unexpected Operand";
                                    return i;
                                }
                                
                                    address = UInt16.Parse(datas[1]);
                                

                                break;
                            }
                        case "nop":
                            {
                                Code = InstructionsOpcode.Nop;
                                if (datas.Count != 1)
                                {
                                    msgError = "Unexpected Operand";
                                    return i;
                                }
                                Code += "00000000000";
                                Binaries[address] = Code;
                                address++;
                                break;
                            }
                        case "setc":
                            {
                                Code = InstructionsOpcode.SETC;
                                if (datas.Count != 1)
                                {
                                    msgError = "Unexpected Operand";
                                    return i;
                                }
                                Code += "00000000000";
                                Binaries[address] = Code;
                                address++;
                                break;
                            }
                        case "clrc":
                            {
                                Code = InstructionsOpcode.CLRC;
                                if (datas.Count != 1)
                                {
                                    msgError = "Unexpected Operand";
                                    return i;
                                }
                                Code += "00000000000";
                                Binaries[address] = Code;
                                address++;
                                break;
                            }
                        case "ldd":
                            {
                                Code = InstructionsOpcode.LDD;
                                if (datas.Count != 3)
                                {
                                    msgError = "Unexpected number of operands";
                                    return i;
                                }

                                string reg = FetchOperand(datas[1]);
                                if (reg == "")
                                {
                                    msgError = "Unexpected operand: " + reg;
                                    return i;
                                }
                                else Code += reg;
                                Code += "000";
                                reg = datas[2];
                                if (reg == "")
                                {
                                    msgError = "Unexpected operand: " + reg;
                                    return i;
                                }
                                else
                                {

                                    String binary = EffectiveHexToBin(reg);
                                    Code += binary;
                                }
                                Code += "1";
                                char[] codeArray = Code.ToCharArray();
                                Code = "";
                                for (int k = 0; k < 16 ; k++)
                                {
                                    Code += codeArray[k];
                                }
                                for (int k = 16; k <32; k++)
                                {
                                    subCode += codeArray[k];
                                }
                                Binaries[address] = Code;
                                address++;
                                Binaries[address] = subCode;
                                address++;

                               
                                break;
                            }
                        case "std":
                            {
                                Code = InstructionsOpcode.STD;
                                if (datas.Count != 3)
                                {
                                    msgError = "Unexpected number of operands";
                                    return i;
                                }
                                Code += "000";
                                string reg = FetchOperand(datas[1]);
                                if (reg == "")
                                {
                                    msgError = "Unexpected operand: " + reg;
                                    return i;
                                }
                                else Code += reg;
                                reg = datas[2];
                                if (reg == "")
                                {
                                    msgError = "Unexpected operand: " + reg;
                                    return i;
                                }
                                else
                                {

                                    String binary = EffectiveHexToBin(reg);
                                    Code += binary;
                                }
                                Code += "1";
                                char[] codeArray = Code.ToCharArray();
                                Code = "";
                                for (int k = 0; k < 16; k++)
                                {
                                    Code += codeArray[k];
                                }
                                for (int k = 16; k < 32; k++)
                                {
                                    subCode += codeArray[k];
                                }
                                Binaries[address] = Code;
                                address++;
                                Binaries[address] = subCode;
                                address++;
                                break;
                            }
                        case "ldm":
                            {
                                Code = InstructionsOpcode.LDM;
                                if (datas.Count != 3)
                                {
                                    msgError = "Unexpected number of operands";
                                    return i;
                                }

                                string reg = FetchOperand(datas[1]);
                                if (reg == "")
                                {
                                    msgError = "Unexpected operand: " + reg;
                                    return i;
                                }
                                else Code += reg;
                                Code += "000";
                                reg = datas[2];
                                if (reg == "")
                                {
                                    msgError = "Unexpected operand: " + reg;
                                    return i;
                                }
                                else
                                {

                                    String binary = ImmediateHexToBin(reg);
                                    Code += binary;
                                }
                                Code += "00001";
                                char[] codeArray = Code.ToCharArray();
                                Code = "";
                                for (int k = 0; k < 16; k++)
                                {
                                    Code += codeArray[k];
                                }
                                for (int k = 16; k < 32; k++)
                                {
                                    subCode += codeArray[k];
                                }
                                Binaries[address] = Code;
                                address++;
                                Binaries[address] = subCode;
                                address++;
                               
                                break;
                            }
                        case "add":
                            {
                                Code = InstructionsOpcode.ADD;
                                if (datas.Count != 4)
                                {
                                    msgError = "Unexpected number of operands";
                                    return i;
                                }

                                string reg = FetchOperand(datas[3]);
                                if (reg == "")
                                {
                                    msgError = "Unexpected operand: " + reg;
                                    return i;
                                }
                                else Code += reg;
                                reg = FetchOperand(datas[1]);
                                if (reg == "")
                                {
                                    msgError = "Unexpected operand: " + reg;
                                    return i;
                                }
                                else Code += reg;
                                reg = FetchOperand(datas[2]);
                                if (reg == "")
                                {
                                    msgError = "Unexpected operand: " + reg;
                                    return i;
                                }
                                else Code += reg;
                                Code += "00";
                                Binaries[address] = Code;
                                address++;

                                break;
                            }
                        case "iadd":
                            {
                                Code = InstructionsOpcode.IADD;
                                if (datas.Count != 4)
                                {
                                    msgError = "Unexpected number of operands";
                                    return i;
                                }

                                string reg = FetchOperand(datas[2]);
                                if (reg == "")
                                {
                                    msgError = "Unexpected operand: " + reg;
                                    return i;
                                }
                                else Code += reg;
                                reg = FetchOperand(datas[1]);
                                if (reg == "")
                                {
                                    msgError = "Unexpected operand: " + reg;
                                    return i;
                                }
                                else Code += reg;
                                reg = datas[3];
                                if (reg == "")
                                {
                                    msgError = "Unexpected operand: " + reg;
                                    return i;
                                }
                                else
                                {

                                    String binary = ImmediateHexToBin(reg);
                                    Code += binary;
                                }
                                Code += "00001";
                                char[] codeArray = Code.ToCharArray();
                                Code = "";
                                for (int k = 0; k < 16 ; k++)
                                {
                                    Code += codeArray[k];
                                }
                                for (int k = 16; k <32; k++)
                                {
                                    subCode += codeArray[k];
                                }
                                Binaries[address] = Code;
                                address++;
                                Binaries[address] = subCode;
                                address++;

                                break;
                            }
                        case "sub":
                            {
                                Code = InstructionsOpcode.SUB;
                                if (datas.Count != 4)
                                {
                                    msgError = "Unexpected number of operands";
                                    return i;
                                }

                                string reg = FetchOperand(datas[3]);
                                if (reg == "")
                                {
                                    msgError = "Unexpected operand: " + reg;
                                    return i;
                                }
                                else Code += reg;
                                reg = FetchOperand(datas[1]);
                                if (reg == "")
                                {
                                    msgError = "Unexpected operand: " + reg;
                                    return i;
                                }
                                else Code += reg;
                                reg = FetchOperand(datas[2]);
                                if (reg == "")
                                {
                                    msgError = "Unexpected operand: " + reg;
                                    return i;
                                }
                                else Code += reg;
                                Code += "00";
                                Binaries[address] = Code;
                                address++;

                                break;
                            }
                        case "and":
                            {
                                Code = InstructionsOpcode.AND;
                                if (datas.Count != 4)
                                {
                                    msgError = "Unexpected number of operands";
                                    return i;
                                }

                                string reg = FetchOperand(datas[3]);
                                if (reg == "")
                                {
                                    msgError = "Unexpected operand: " + reg;
                                    return i;
                                }
                                else Code += reg;
                                reg = FetchOperand(datas[1]);
                                if (reg == "")
                                {
                                    msgError = "Unexpected operand: " + reg;
                                    return i;
                                }
                                else Code += reg;
                                reg = FetchOperand(datas[2]);
                                if (reg == "")
                                {
                                    msgError = "Unexpected operand: " + reg;
                                    return i;
                                }
                                else Code += reg;
                                Code += "00";
                                Binaries[address] = Code;
                                address++;

                                break;
                            }

                        case "or":
                            {
                                Code = InstructionsOpcode.OR;
                                if (datas.Count != 4)
                                {
                                    msgError = "Unexpected number of operands";
                                    return i;
                                }

                                string reg = FetchOperand(datas[3]);
                                if (reg == "")
                                {
                                    msgError = "Unexpected operand: " + reg;
                                    return i;
                                }
                                else Code += reg;
                                reg = FetchOperand(datas[1]);
                                if (reg == "")
                                {
                                    msgError = "Unexpected operand: " + reg;
                                    return i;
                                }
                                else Code += reg;
                                reg = FetchOperand(datas[2]);
                                if (reg == "")
                                {
                                    msgError = "Unexpected operand: " + reg;
                                    return i;
                                }
                                else Code += reg;
                                Code += "00";
                                Binaries[address] = Code;
                                address++;

                                break;
                            }
                        case "swap":
                            {
                                Code = InstructionsOpcode.SWAP;
                                if (datas.Count != 3)
                                {
                                    msgError = "Unexpected number of operands";
                                    return i;
                                }

                                string reg = FetchOperand(datas[2]);
                                if (reg == "")
                                {
                                    msgError = "Unexpected operand: " + reg;
                                    return i;
                                }
                                else Code += reg;
                                reg = FetchOperand(datas[1]);
                                if (reg == "")
                                {
                                    msgError = "Unexpected operand: " + reg;
                                    return i;
                                }
                                else Code += reg;
                                Code += "00000";
                                Binaries[address] = Code;
                                address++;

                                break;
                            }
                        case "not":
                            {
                                Code = InstructionsOpcode.NOT;
                                if (datas.Count != 2)
                                {
                                    msgError = "Unexpected number of operands";
                                    return i;
                                }
                                Code += "000";
                                string reg = FetchOperand(datas[1]);
                                if (reg == "")
                                {
                                    msgError = "Unexpected operand: " + reg;
                                    return i;
                                }
                                else Code += reg;

                                Code += "00000";

                                Binaries[address] = Code;
                                address++;

                                break;
                            }
                        case "inc":
                            {
                                Code = InstructionsOpcode.INC;
                                if (datas.Count != 2)
                                {
                                    msgError = "Unexpected number of operands";
                                    return i;
                                }
                                Code += "000";
                                string reg = FetchOperand(datas[1]);
                                if (reg == "")
                                {
                                    msgError = "Unexpected operand: " + reg;
                                    return i;
                                }
                                else Code += reg;

                                Code += "00000";

                                Binaries[address] = Code;
                                address++;

                                break;
                            }
                        case "dec":
                            {
                                Code = InstructionsOpcode.DEC;
                                if (datas.Count != 2)
                                {
                                    msgError = "Unexpected number of operands";
                                    return i;
                                }
                                Code += "000";
                                string reg = FetchOperand(datas[1]);
                                if (reg == "")
                                {
                                    msgError = "Unexpected operand: " + reg;
                                    return i;
                                }
                                else Code += reg;

                                Code += "00000";

                                Binaries[address] = Code;
                                address++;

                                break;
                            }
                        case "shr":
                            {
                                Code = InstructionsOpcode.SHR;
                                if (datas.Count != 3)
                                {
                                    msgError = "Unexpected number of operands";
                                    return i;
                                }
                                Code += "000";
                                string reg = FetchOperand(datas[1]);
                                if (reg == "")
                                {
                                    msgError = "Unexpected operand: " + reg;
                                    return i;
                                }
                                else Code += reg;
                                reg = datas[2];
                                if (reg == "")
                                {
                                    msgError = "Unexpected operand: " + reg;
                                    return i;
                                }
                                else
                                {

                                    String binary = ImmediateHexToBin(reg);
                                    Code += binary;
                                }
                                Code += "00001";
                                char[] codeArray = Code.ToCharArray();
                                Code = "";
                                for (int k = 0; k < 16; k++)
                                {
                                    Code += codeArray[k];
                                }
                                for (int k = 16; k < 32; k++)
                                {
                                    subCode += codeArray[k];
                                }
                                Binaries[address] = Code;
                                address++;
                                Binaries[address] = subCode;
                                address++;
                                break;

                            }
                        case "shl":
                            {
                                Code = InstructionsOpcode.SHL;
                                if (datas.Count != 3)
                                {
                                    msgError = "Unexpected number of operands";
                                    return i;
                                }
                                Code += "000";
                                string reg = FetchOperand(datas[1]);
                                if (reg == "")
                                {
                                    msgError = "Unexpected operand: " + reg;
                                    return i;
                                }
                                else Code += reg;
                                 reg = datas[2];
                                 if (reg == "")
                                {
                                    msgError = "Unexpected operand: " + reg;
                                    return i;
                                }
                                 else
                                {

                                    String binary = ImmediateHexToBin(reg);
                                    Code += binary;
                                }
                                Code += "00001";
                                char[] codeArray = Code.ToCharArray();
                                Code = "";
                                for (int k = 0; k < 16 ; k++)
                                {
                                    Code += codeArray[k];
                                }
                                for (int k = 16; k <32; k++)
                                {
                                    subCode += codeArray[k];
                                }
                                Binaries[address] = Code;
                                address++;
                                Binaries[address] = subCode;
                                address++;
                                break;
                            }
                        case "push":
                            {
                                Code = InstructionsOpcode.PUSH;

                                if (datas.Count != 2)
                                {
                                    msgError = "Unexpected number of operands";
                                    return i;
                                }
                                Code += "000";
                                string reg = FetchOperand(datas[1]);
                                if (reg == "")
                                {
                                    msgError = "Unexpected operand: " + reg;
                                    return i;
                                }
                                else Code += reg;

                                Code += "00000";

                                Binaries[address] = Code;
                                address++;

                                break;
                            }
                        case "pop":
                            {
                                Code = InstructionsOpcode.POP;

                                if (datas.Count != 2)
                                {
                                    msgError = "Unexpected number of operands";
                                    return i;
                                }

                                string reg = FetchOperand(datas[1]);
                                if (reg == "")
                                {
                                    msgError = "Unexpected operand: " + reg;
                                    return i;
                                }
                                else Code += reg;
                                Code += "00000000";

                                Binaries[address] = Code;
                                address++;

                                break;
                            }
                        case "out":
                            {
                                Code = InstructionsOpcode.OUT;

                                if (datas.Count != 2)
                                {
                                    msgError = "Unexpected number of operands";
                                    return i;
                                }
                                Code += "000";
                                string reg = FetchOperand(datas[1]);
                                if (reg == "")
                                {
                                    msgError = "Unexpected operand: " + reg;
                                    return i;
                                }
                                else Code += reg;

                                Code += "00000";

                                Binaries[address] = Code;
                                address++;

                                break;
                            }
                        case "in":
                            {
                                Code = InstructionsOpcode.IN;

                                if (datas.Count != 2)
                                {
                                    msgError = "Unexpected number of operands";
                                    return i;
                                }

                                string reg = FetchOperand(datas[1]);
                                if (reg == "")
                                {
                                    msgError = "Unexpected operand: " + reg;
                                    return i;
                                }
                                else Code += reg;

                                Code += "00000000";

                                Binaries[address] = Code;
                                address++;

                                break;
                            }
                        case "jmp":
                            {
                                Code = InstructionsOpcode.JMP;
                                if (datas.Count != 2)
                                {
                                    msgError = "Unexpected number of operands";
                                    return i;
                                }
                                Code += "000";
                                string reg = FetchOperand(datas[1]);
                                if (reg == "")
                                {
                                    msgError = "Unexpected operand: " + reg;
                                    return i;
                                }
                                else Code += reg;

                                Code += "00000";
                                Binaries[address] = Code;
                                address++;

                                break;
                            }
                        case "jz":
                            {
                                Code = InstructionsOpcode.JZ;
                                if (datas.Count != 2)
                                {
                                    msgError = "Unexpected number of operands";
                                    return i;
                                }
                                Code += "000";
                                string reg = FetchOperand(datas[1]);
                                if (reg == "")
                                {
                                    msgError = "Unexpected operand: " + reg;
                                    return i;
                                }
                                else Code += reg;
                                Code += "00000";
                                Binaries[address] = Code;
                                address++;

                                break;
                            }
                        case "jn":
                            {
                                Code = InstructionsOpcode.JN;
                                if (datas.Count != 2)
                                {
                                    msgError = "Unexpected number of operands";
                                    return i;
                                }
                                Code += "000";
                                string reg = FetchOperand(datas[1]);
                                if (reg == "")
                                {
                                    msgError = "Unexpected operand: " + reg;
                                    return i;
                                }
                                else Code += reg;
                                Code += "00000";
                                Binaries[address] = Code;
                                address++;

                                break;
                            }
                        case "jc":
                            {
                                Code = InstructionsOpcode.JC;
                                if (datas.Count != 2)
                                {
                                    msgError = "Unexpected number of operands";
                                    return i;
                                }
                                Code += "000";
                                string reg = FetchOperand(datas[1]);
                                if (reg == "")
                                {
                                    msgError = "Unexpected operand: " + reg;
                                    return i;
                                }
                                else Code += reg;
                                Code += "00000";
                                Binaries[address] = Code;
                                address++;

                                break;
                            }
                        case "call":
                            {
                                Code = InstructionsOpcode.CALL;
                                if (datas.Count != 2)
                                {
                                    msgError = "Unexpected number of operands";
                                    return i;
                                }
                                Code += "000";
                                string reg = FetchOperand(datas[1]);
                                if (reg == "")
                                {
                                    msgError = "Unexpected operand: " + reg;
                                    return i;
                                }
                                else Code += reg;
                                Code += "00000";
                                Binaries[address] = Code;
                                address++;

                                break;
                            }
                        case "ret":
                            {
                                Code = InstructionsOpcode.RET;
                                if (datas.Count != 1)
                                {
                                    msgError = "Unexpected number of operands";
                                    return i;
                                }
                                Code += "00000000000";

                                Binaries[address] = Code;
                                address++;

                                break;
                            }
                        case "rti":
                            {
                                Code = InstructionsOpcode.RTI;
                                if (datas.Count != 1)
                                {
                                    msgError = "Unexpected number of operands";
                                    return i;
                                }

                                Code += "00000000000";

                                Binaries[address] = Code;
                                address++;

                                break;
                            }
                        default:
                            {
                                String binary = HexToBin(datas[0]);
                                Code += binary;
                                Binaries[address] = Code;
                                address++;
                                break;
                            }
                    }
                    if (address > 4095)
                    {
                        msgError = "you exceed the limit of the memory. The memory is 4096 bytes only";
                        return i;
                    }
                }

                int index = text.LastIndexOf('.');
                string path = text.Substring(0, index + 1);
                path += "mem";
                writeFile.WriteFile(path, Binaries);
                return -1;
            }
            catch (Exception exc)
            {
                return i;
            }

        }


        public string FetchOperand(string s)
        {
            if (s == "r0")
                return "000";
            else if (s == "r1")
                return "001";
            else if (s == "r2")
                return "010";
            else if (s == "r3")
                return "011";
            else if (s == "r4")
                return "100";
            else if (s == "r5")
                return "101";
            else if (s == "r6")
                return "110";
            else if (s == "r7")
                return "111";
            else return "";
        }

        static string ImmediateHexToBin(string s)
        {
            char[] hexdec = s.ToCharArray();
            string Code=""; 
            int i = 0;

            while (i < hexdec.Length)
            {
                
                switch (hexdec[i])
                {
                    case '0':
                        Code += "0000";
                        break;
                    case '1':
                        Code += "0001";
                        break;
                    case '2':
                        Code +="0010";
                        break;
                    case '3':
                        Code += "0011";
                        break;
                    case '4':
                        Code += "0100";
                        break;
                    case '5':
                        Code +="0101";
                        break;
                    case '6':
                        Code +="0110";
                        break;
                    case '7':
                        Code +="0111";
                        break;
                    case '8':
                        Code +="1000";
                        break;
                    case '9':
                        Code +="1001";
                        break;
                    case 'A':
                    case 'a':
                        Code +="1010";
                        break;
                    case 'B':
                    case 'b':
                        Code +="1011";
                        break;
                    case 'C':
                    case 'c':
                        Code +="1100";
                        break;
                    case 'D':
                    case 'd':
                        Code +="1101";
                        break;
                    case 'E':
                    case 'e':
                        Code +="1110";
                        break;
                    case 'F':
                    case 'f':
                        Code +="1111";
                        break;
                }
                i++;
            }
            if (i == 1)
            {
                Code += "000000000000";
            }
            else if(i== 2)
            {
                Code += "00000000";
            }
            else if (i == 3)
            {
                Code += "0000";
            }
            return Code;
        }

        static string HexToBin(string s)
        {
            char[] hexdec = s.ToCharArray();
            string Code = "";
            int i = 0;

            while (i < hexdec.Length)
            {

                switch (hexdec[i])
                {
                    case '0':
                        Code += "0000";
                        break;
                    case '1':
                        Code += "0001";
                        break;
                    case '2':
                        Code += "0010";
                        break;
                    case '3':
                        Code += "0011";
                        break;
                    case '4':
                        Code += "0100";
                        break;
                    case '5':
                        Code += "0101";
                        break;
                    case '6':
                        Code += "0110";
                        break;
                    case '7':
                        Code += "0111";
                        break;
                    case '8':
                        Code += "1000";
                        break;
                    case '9':
                        Code += "1001";
                        break;
                    case 'A':
                    case 'a':
                        Code += "1010";
                        break;
                    case 'B':
                    case 'b':
                        Code += "1011";
                        break;
                    case 'C':
                    case 'c':
                        Code += "1100";
                        break;
                    case 'D':
                    case 'd':
                        Code += "1101";
                        break;
                    case 'E':
                    case 'e':
                        Code += "1110";
                        break;
                    case 'F':
                    case 'f':
                        Code += "1111";
                        break;
                }
                i++;
            }
            if (i == 1)
            {
                Code = "000000000000" + Code; ;
            }
            else if (i == 2)
            {
                Code = "00000000" + Code;
            }
            else if (i == 3)
            {
                Code = "0000" + Code;
            }
            return Code;
        }

        static string EffectiveHexToBin(string s)
        {
            char[] hexdec = s.ToCharArray();
            string Code = "";
            int i = 0;

            while (i < hexdec.Length)
            {

                switch (hexdec[i])
                {
                    case '0':
                        Code += "0000";
                        break;
                    case '1':
                        Code += "0001";
                        break;
                    case '2':
                        Code += "0010";
                        break;
                    case '3':
                        Code += "0011";
                        break;
                    case '4':
                        Code += "0100";
                        break;
                    case '5':
                        Code += "0101";
                        break;
                    case '6':
                        Code += "0110";
                        break;
                    case '7':
                        Code += "0111";
                        break;
                    case '8':
                        Code += "1000";
                        break;
                    case '9':
                        Code += "1001";
                        break;
                    case 'A':
                    case 'a':
                        Code += "1010";
                        break;
                    case 'B':
                    case 'b':
                        Code += "1011";
                        break;
                    case 'C':
                    case 'c':
                        Code += "1100";
                        break;
                    case 'D':
                    case 'd':
                        Code += "1101";
                        break;
                    case 'E':
                    case 'e':
                        Code += "1110";
                        break;
                    case 'F':
                    case 'f':
                        Code += "1111";
                        break;
                }
                i++;
            }
            if (i == 1)
            {
                Code += "0000000000000000";
            }
            else if (i == 2)
            {
                Code += "000000000000";
            }
            else if (i == 3)
            {
                Code += "00000000";
            }
            else if (i == 4)
            {
                Code += "0000";
            }
            return Code;
        } 
    }
}
