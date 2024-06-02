using System;
using System.Collections.Generic;
using System.IO;

class Assembler
{
    static byte[] mem = new byte[65536];

    static Dictionary<string, string> instructionMap = new Dictionary<string, string>
    {
        {"MOV","0000"},
        {"ADD", "0001"},
        {"SUB", "0010"},
        {"CMP", "0011"},
        {"AND", "0100"},
        {"OR", "0101"},
        {"XOR", "0110"},
        {"CLR", "1000000000"},
        {"NEG", "1000000001"},
        {"INC", "1000000010"},
        {"DEC", "1000000011"},
        {"ASL", "1000000100"},
        {"ASR", "1000000101"},
        {"LSR", "1000000110"},
        {"ROL", "1000000111"},
        {"ROR", "1000001000"},
        {"RLC", "1000001001"},
        {"RRC", "1000001010"},
        {"JMP", "1000001011"},
        {"CALL", "1000001100"},
        {"PUSH", "1000001101"},
        {"POP", "1000001110"},
        {"BR", "11000000"},
        {"BNE", "11000001"},
        {"BEQ", "11000010"},
        {"BPL", "11000011"},
        {"BMI", "11000100"},
        {"BCS", "11000101"},
        {"BCC", "11000110"},
        {"BVS", "11000111"},
        {"BVC", "11001000"},
        {"CLC", "1110000000000000"},
        {"CLV", "1110000000000001"},
        {"CLZ", "1110000000000010"},
        {"CLS", "1110000000000011"},
        {"CCC", "1110000000000100"},
        {"SEC", "1110000000000101"},
        {"SEV", "1110000000000110"},
        {"SEZ", "1110000000000111"},
        {"SES", "1110000000001000"},
        {"SCC", "1110000000001001"},
        {"NOP", "1110000000001010"},
        {"RET", "1110000000001011"},
        {"RETI", "1110000000001100"},
        {"HALT", "1110000000001101"},
        {"WAIT", "1110000000001110"},
        {"PUSH PC", "1110000000001111"},
        {"POP PC", "1110000000010000"},
        {"PUSH FLAG", "1110000000010001"},
        {"POP FLAG", "1110000000010010"}
    };

    static Dictionary<string, string> registerMap = new Dictionary<string, string>
    {
        {"R0", "0000"},
        {"R1", "0001"},
        {"R2", "0010"},
        {"R3", "0011"},
        {"R4", "0100"},
        {"R5", "0101"},
        {"R6", "0110"},
        {"R7", "0111"},
        {"R8", "1000"},
        {"R9", "1001"},
        {"R10", "1010"},
        {"R11", "1011"},
        {"R12", "1100"},
        {"R13", "1101"},
        {"R14", "1110"},
        {"R15", "1111"}
    };

    static Dictionary<string, string> addressingModeMap = new Dictionary<string, string>
    {
        {"AM", "00"},
        {"AD", "01"},
        {"AI", "10"},
        {"AX", "11"}
    };

    static void Main(string[] args)
    {
        string filePath = "D:\\Visual Studio Saves\\Asamblor\\Asamblor\\Test.asm";

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
                int memIndex = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    string binaryInstruction = ParseInstruction(line);
                    if (!string.IsNullOrEmpty(binaryInstruction))
                    {
                        int instructionValue = Convert.ToInt32(binaryInstruction, 2);
                        mem[memIndex++] = (byte)(instructionValue >> 8);
                        mem[memIndex++] = (byte)(instructionValue & 0xFF);
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

    static string ParseInstruction(string line)
    {
        // Remove comments and trim the line
        line = line.Split('#')[0].Trim();

        if (string.IsNullOrEmpty(line))
        {
            return null;
        }

        string[] parts = line.Split(' ', ',', '(', ')');
        string instruction = parts[0];
        string binaryInstruction = "";

        if (instructionMap.ContainsKey(instruction))
        {
            binaryInstruction = instructionMap[instruction];
        }

        var lastThreeBites = binaryInstruction[..2];

        switch (lastThreeBites[0])
        {
            case '0':
                break;

            case '1':
                switch (lastThreeBites)
                {
                    case "100":
                        break;
                    case "110":
                        break;
                    case "111":
                        break;
                }
                break;
        }

        for (int i = 1; i < parts.Length; i++)
        {
            if (registerMap.ContainsKey(parts[i]))
            {
                binaryInstruction += registerMap[parts[i]];
            }
            else if (addressingModeMap.ContainsKey(parts[i]))
            {
                binaryInstruction += addressingModeMap[parts[i]];
            }
            else if (int.TryParse(parts[i], out int number))
            {
                binaryInstruction += Convert.ToString(number, 2).PadLeft(4, '0');
            }
        }

        return binaryInstruction;
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

    static long BinaryToNumber(string binaryStr)
    {
        // Convert binary string to a number
        long number = Convert.ToInt64(binaryStr, 2);
        return number;
    }
}
