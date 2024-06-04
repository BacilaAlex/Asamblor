class Assembler
{
    static int PC = 0;
    static readonly byte[] mem = new byte[65536];

    static readonly Dictionary<string, int> instructionMap = new Dictionary<string, int>
    {
        //b1
        {"MOV", Convert.ToInt32("0000", 2)},
        {"ADD", Convert.ToInt32("0001", 2)},
        {"SUB", Convert.ToInt32("0010", 2)},
        {"CMP", Convert.ToInt32("0011", 2)},
        {"AND", Convert.ToInt32("0100", 2)},
        {"OR", Convert.ToInt32("0101", 2)},
        {"XOR", Convert.ToInt32("0110", 2)},
        //b2
        {"CLR", Convert.ToInt32("1000000000", 2)},
        {"NEG", Convert.ToInt32("1000000001", 2)},
        {"INC", Convert.ToInt32("1000000010", 2)},
        {"DEC", Convert.ToInt32("1000000011", 2)},
        {"ASL", Convert.ToInt32("1000000100", 2)},
        {"ASR", Convert.ToInt32("1000000101", 2)},
        {"LSR", Convert.ToInt32("1000000110", 2)},
        {"ROL", Convert.ToInt32("1000000111", 2)},
        {"ROR", Convert.ToInt32("1000001000", 2)},
        {"RLC", Convert.ToInt32("1000001001", 2)},
        {"RRC", Convert.ToInt32("1000001010", 2)},
        {"JMP", Convert.ToInt32("1000001011", 2)},
        {"CALL", Convert.ToInt32("1000001100", 2)},
        {"PUSH", Convert.ToInt32("1000001101", 2)},
        {"POP", Convert.ToInt32("1000001110", 2)},
        //b3
        {"BR", Convert.ToInt32("11000000", 2)},
        {"BNE", Convert.ToInt32("11000001", 2)},
        {"BEQ", Convert.ToInt32("11000010", 2)},
        {"BPL", Convert.ToInt32("11000011", 2)},
        {"BMI", Convert.ToInt32("11000100", 2)},
        {"BCS", Convert.ToInt32("11000101", 2)},
        {"BCC", Convert.ToInt32("11000110", 2)},
        {"BVS", Convert.ToInt32("11000111", 2)},
        {"BVC", Convert.ToInt32("11001000", 2)},
        //b4
        {"CLC", Convert.ToInt32("1110000000000000", 2)},
        {"CLV", Convert.ToInt32("1110000000000001", 2)},
        {"CLZ", Convert.ToInt32("1110000000000010", 2)},
        {"CLS", Convert.ToInt32("1110000000000011", 2)},
        {"CCC", Convert.ToInt32("1110000000000100", 2)},
        {"SEC", Convert.ToInt32("1110000000000101", 2)},
        {"SEV", Convert.ToInt32("1110000000000110", 2)},
        {"SEZ", Convert.ToInt32("1110000000000111", 2)},
        {"SES", Convert.ToInt32("1110000000001000", 2)},
        {"SCC", Convert.ToInt32("1110000000001001", 2)},
        {"NOP", Convert.ToInt32("1110000000001010", 2)},
        {"RET", Convert.ToInt32("1110000000001011", 2)},
        {"RETI", Convert.ToInt32("1110000000001100", 2)},
        {"HALT", Convert.ToInt32("1110000000001101", 2)},
        {"WAIT", Convert.ToInt32("1110000000001110", 2)},
        {"PUSH PC", Convert.ToInt32("1110000000001111", 2)},
        {"POP PC", Convert.ToInt32("1110000000010000", 2)},
        {"PUSH FLAG", Convert.ToInt32("1110000000010001", 2)},
        {"POP FLAG", Convert.ToInt32("1110000000010010", 2)}
    };
    static readonly Dictionary<string, byte[]> instructionByteMap = new Dictionary<string, byte[]>
    {
        //b1
        {"MOV", GetBytes("0000")},
        {"ADD", GetBytes("0001")},
        {"SUB", GetBytes("0010")},
        {"CMP", GetBytes("0011")},
        {"AND", GetBytes("0100")},
        {"OR", GetBytes("0101")},
        {"XOR", GetBytes("0110")},
        //b2
        {"CLR", GetBytes("1000000000")},
        {"NEG", GetBytes("1000000001")},
        {"INC", GetBytes("1000000010")},
        {"DEC", GetBytes("1000000011")},
        {"ASL", GetBytes("1000000100")},
        {"ASR", GetBytes("1000000101")},
        {"LSR", GetBytes("1000000110")},
        {"ROL", GetBytes("1000000111")},
        {"ROR", GetBytes("1000001000")},
        {"RLC", GetBytes("1000001001")},
        {"RRC", GetBytes("1000001010")},
        {"JMP", GetBytes("1000001011")},
        {"CALL", GetBytes("1000001100")},
        {"PUSH", GetBytes("1000001101")},
        {"POP", GetBytes("1000001110")},
        //b3
        {"BR", GetBytes("11000000")},
        {"BNE", GetBytes("11000001")},
        {"BEQ", GetBytes("11000010")},
        {"BPL", GetBytes("11000011")},
        {"BMI", GetBytes("11000100")},
        {"BCS", GetBytes("11000101")},
        {"BCC", GetBytes("11000110")},
        {"BVS", GetBytes("11000111")},
        {"BVC", GetBytes("11001000")},
        //b4
        {"CLC", GetBytes("1110000000000000")},
        {"CLV", GetBytes("1110000000000001")},
        {"CLZ", GetBytes("1110000000000010")},
        {"CLS", GetBytes("1110000000000011")},
        {"CCC", GetBytes("1110000000000100")},
        {"SEC", GetBytes("1110000000000101")},
        {"SEV", GetBytes("1110000000000110")},
        {"SEZ", GetBytes("1110000000000111")},
        {"SES", GetBytes("1110000000001000")},
        {"SCC", GetBytes("1110000000001001")},
        {"NOP", GetBytes("1110000000001010")},
        {"RET", GetBytes("1110000000001011")},
        {"RETI", GetBytes("1110000000001100")},
        {"HALT", GetBytes("1110000000001101")},
        {"WAIT", GetBytes("1110000000001110")},
        {"PUSH PC", GetBytes("1110000000001111")},
        {"POP PC", GetBytes("1110000000010000")},
        {"PUSH FLAG", GetBytes("1110000000010001")},
        {"POP FLAG", GetBytes("1110000000010010")}
    };
    static byte[] GetBytes(string binaryString)
    {
        var counrrSHL = 16 - binaryString.Length;
        var number = Convert.ToInt32(binaryString, 2);
        number <<= counrrSHL;
        return BitConverter.GetBytes(number).Take(2).ToArray();
    }
    static readonly Dictionary<string, int> registerMap = new Dictionary<string, int>
    {
        {"R0", Convert.ToInt32("0000", 2)},
        {"R1", Convert.ToInt32("0001", 2)},
        {"R2", Convert.ToInt32("0010", 2)},
        {"R3", Convert.ToInt32("0011", 2)},
        {"R4", Convert.ToInt32("0100", 2)},
        {"R5", Convert.ToInt32("0101", 2)},
        {"R6", Convert.ToInt32("0110", 2)},
        {"R7", Convert.ToInt32("0111", 2)},
        {"R8", Convert.ToInt32("1000", 2)},
        {"R9", Convert.ToInt32("1001", 2)},
        {"R10", Convert.ToInt32("1010", 2)},
        {"R11", Convert.ToInt32("1011", 2)},
        {"R12", Convert.ToInt32("1100", 2)},
        {"R13", Convert.ToInt32("1101", 2)},
        {"R14", Convert.ToInt32("1110", 2)},
        {"R15", Convert.ToInt32("1111", 2)}
    };
    static readonly Dictionary<string, int> addressingModeMap = new Dictionary<string, int>
    {
        {"AM", Convert.ToInt32("00", 2)},
        {"AD", Convert.ToInt32("01", 2)},
        {"AI", Convert.ToInt32("10", 2)},
        {"AX", Convert.ToInt32("11", 2)}
    };
    static readonly Dictionary<string, int> addressingET = [];

    static void Main(string[] args)
    {
        string filePath = "E:\\Visual Studio\\Visual Studio Saves\\Asamblor\\Asamblor\\Test.asm";

        if (!File.Exists(filePath))
        {
            Console.WriteLine("File does not exist.");
            return;
        }

        try
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var instructionBytes = ParseInstruction(line);
                    if (instructionBytes != null)
                    {
                        foreach (var bytePart in instructionBytes)
                        {
                            mem[PC++] = bytePart;
                        }
                    }
                }
            }

            PrintMemoryContents();
        }
        catch (Exception e)
        {
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }
    }



    static List<byte>? ParseInstruction(string line)
    {
        int IR = -1, MAS = -1, RS, MAD = -1, RD, OFFSET = -1;
        int indexLine = 0;
        var addMem = new List<byte>();

        // Remove comments and trim the line
        line = line.Split(['#', ';'])[0].Trim();

        if (string.IsNullOrEmpty(line))
        {
            return null;
        }

        string[] parts = line.Split(new[] { ' ', ',', ':', ';', '.' }, StringSplitOptions.RemoveEmptyEntries).Select(l => l.Trim()).ToArray();

        if (instructionMap.ContainsKey(parts[0]))
        {
            IR = instructionMap[parts[0]];
        }
        else
        {
            PopulateDictionaryET(parts[0]);
            indexLine++;
        }

        var instruction = parts[indexLine];

        var byteInstruction = instructionByteMap[instruction];

        var lastBite = byteInstruction[^1] & 0x80;
        var lastThreeBites = byteInstruction[^1] & 0xE0;

        if (lastBite.Equals(0x0))
        {
            //b1
            (MAD, RD) = GetMADandRD(parts, indexLine);
            indexLine++;
            (MAS, RS) = GetMASandRS(parts, indexLine);

            IR <<= 2;
            IR += MAS;
            IR <<= 4;
            IR += RS;
            IR <<= 2;
            IR += MAD;
            IR <<= 4;
            IR += RD;


            addMem.AddRange(BitConverter.GetBytes(IR).Take(2).ToList());

            //byte array
            if (MAS.Equals(3))
            {
                var partInstruction = parts[indexLine + 1];
                int.TryParse(partInstruction[..partInstruction.IndexOf('(')], out int number);
                var byteArray = BitConverter.GetBytes(number).Take(2).ToList();
                addMem.AddRange(byteArray);
            }
            else if (MAS.Equals(0))
            {
                var partInstruction = parts[indexLine + 1];
                int.TryParse(partInstruction, out int number);
                var byteArray = BitConverter.GetBytes(number).Take(2).ToList();
                addMem.AddRange(byteArray);
            }
            if (MAD.Equals(3))
            {
                var partInstruction = parts[indexLine];
                int.TryParse(partInstruction[..partInstruction.IndexOf('(')], out int number);
                var byteArray = BitConverter.GetBytes(number).Take(2).ToList();
                addMem.AddRange(byteArray);
            }
        }
        else
        {
            switch (lastThreeBites)
            {
                case 0x80:
                    //b2
                    var lastPart = parts[parts.Length - 1];
                    if (!char.IsDigit(lastPart[lastPart.Length - 1]) && !lastPart[lastPart.Length - 1].Equals(')'))
                    {
                        addMem.AddRange(BitConverter.GetBytes(IR).Take(2).ToList());

                        var adr = CalculateTheOffset(PC, parts[indexLine + 1]);

                        var byteArray = BitConverter.GetBytes(adr).Take(2).ToList();
                        addMem.AddRange(byteArray);
                    }
                    else
                    {
                        (MAD, RD) = GetMADandRD(parts, indexLine);
                        IR <<= 2;
                        IR += MAD;
                        IR <<= 4;
                        IR += RD;

                        addMem.AddRange(BitConverter.GetBytes(IR).Take(2).ToList());

                        if (MAD.Equals(3))
                        {
                            var partInstruction = parts[indexLine + 1];
                            int.TryParse(partInstruction[..partInstruction.IndexOf('(')], out int number);
                            var byteArray = BitConverter.GetBytes(number).Take(2).ToList();
                            addMem.AddRange(byteArray);
                        }
                    }

                    break;
                case 0xC0:
                    //b3
                    IR <<= 8;
                    OFFSET = CalculateTheOffset(PC, parts[1]);
                    IR += OFFSET;
                    addMem.AddRange(BitConverter.GetBytes(IR).Take(2).ToList());
                    break;
                case 0xE0:
                    //b4
                    IR = IR;
                    addMem.AddRange(BitConverter.GetBytes(IR).Take(2).ToList());
                    break;
            }
        }

        return addMem;
    }

    static void PopulateDictionaryET(string ET)
    {
        addressingET.Add(ET, PC);
    }

    static int CalculateTheOffset(int PC, string adr)
    {
        if (addressingET.ContainsKey(adr))
        {
            return addressingET[adr] - PC;
        }
        else
        {
            adr = adr.Substring(0, adr.Length - 1);
            return Convert.ToInt32(adr, 16);
        }
    }

    static (int MAS, int RS) GetMASandRS(string[] parts, int indexLine)
    {
        int MAS;
        int RS;
        //MAS & RS
        if (registerMap.ContainsKey(parts[indexLine + 1]))
        {
            MAS = addressingModeMap["AD"];
            RS = registerMap[parts[indexLine + 1]];
        }
        else
        {
            parts = parts.Concat(parts[indexLine + 1].Split(new char[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries)).ToArray();

            var tempList = parts.ToList();
            tempList.RemoveAt(indexLine + 1);
            parts = tempList.ToArray();

            if (registerMap.ContainsKey(parts[indexLine + 1]))
            {
                MAS = addressingModeMap["AI"];
                RS = registerMap[parts[indexLine + 1]];
            }
            else if (parts.Length > indexLine + 2 && registerMap.ContainsKey(parts[indexLine + 2]))
            {
                MAS = addressingModeMap["AX"];
                RS = registerMap[parts[indexLine + 2]];
            }
            else
            {
                MAS = addressingModeMap["AM"];
                RS = Int32.Parse(parts[indexLine + 1]);
            }
        }

        return (MAS, RS);
    }

    static (int MAD, int RD) GetMADandRD(string[] parts, int indexLine)
    {
        int MAD;
        int RD;
        //MAD & RD
        if (registerMap.ContainsKey(parts[indexLine + 1]))
        {
            MAD = addressingModeMap["AD"];
            RD = registerMap[parts[indexLine + 1]];
        }
        else
        {
            parts = parts.Concat(parts[indexLine + 1].Split(new char[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries)).ToArray();

            var tempList = parts.ToList();
            tempList.RemoveAt(indexLine + 1);
            parts = tempList.ToArray();
            if (tempList.Count > 3 + indexLine)
                indexLine++;

            if (registerMap.ContainsKey(parts[indexLine + 1]))
            {
                MAD = addressingModeMap["AI"];
                RD = registerMap[parts[indexLine + 1]];
            }
            else
            {
                parts[indexLine + 2] = parts[indexLine + 2].Trim(['(', ')']);

                MAD = addressingModeMap["AX"];
                RD = registerMap[parts[indexLine + 2]];
            }
        }
        return (MAD, RD);
    }

    static void PrintMemoryContents()
    {
        Console.WriteLine("Memory Contents:");
        for (int i = 0; i < mem.Length; i += 16)
        {
            Console.Write("{0:X4}: ", i);
            for (int j = 0; j < 16; j++)
            {
                if (i + j < mem.Length)
                {
                    Console.Write("{0:X2} ", mem[i + j]);
                }
            }
            Console.WriteLine();
        }
    }
}
