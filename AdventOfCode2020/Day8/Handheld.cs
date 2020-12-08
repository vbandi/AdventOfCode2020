using System;
using System.Collections.Generic;

namespace Day8
{
    public class Handheld
    {
        public List<Instruction> Memory { get; } = new ();
        public int Accumulator { get; private set; } = 0;
        public int PC { get; private set; } = 0;

        public void Compile(string input)
        {
            foreach (var line in input.Split(Environment.NewLine.ToCharArray()))
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;
                
                // Console.WriteLine($"Processing '{line}'");
                if (!Enum.TryParse(line[..3], out Operation op))
                    throw new InvalidOperationException(line[..3]);

                if (!int.TryParse(line[4..], out int argument))
                    throw new ArgumentException(line);
                
                Memory.Add(new Instruction(op, argument));
            }
        }

        
        /// <summary>
        /// Executes the instructions in the memory
        /// </summary>
        /// <returns>true if the program finished - false if infinite loop was detected</returns>
        public bool Run()
        {
            List<int> ExecutedInstructions = new();
            
            PC = 0;
            Accumulator = 0;

            while (PC < Memory.Count)
            {
                if (ExecutedInstructions.Contains(PC))
                    return false;

                ExecutedInstructions.Add(PC);

                if (Execute(Memory[PC]))
                    PC++;
            }
            
            Console.WriteLine("Program reached end.");
            return true;
        }

        /// <summary>
        /// Executes the specified instruction
        /// </summary>
        /// <returns>true, if the PC needs to be incremented</returns>
        /// <exception cref="InvalidOperationException"></exception>
        private bool Execute(Instruction instruction)
        {
            switch (instruction.Operation)
            {
                case Operation.acc:
                    Accumulator += instruction.Argument;
                    return true;
                case Operation.jmp:
                    PC += instruction.Argument;
                    return false;
                case Operation.nop:
                    return true;
                default:
                    throw new InvalidOperationException();
            }
        }
    }
    
    public record Instruction(Operation Operation, int Argument);

    public enum Operation
    {
        // ReSharper disable InconsistentNaming
        acc, jmp, nop
        // ReSharper restore InconsistentNaming
    }
}