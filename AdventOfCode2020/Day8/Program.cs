using System;
using System.IO;
using Day8;

var input = File.ReadAllText("Input.txt");
var handheld = new Handheld();
handheld.Compile(input);
handheld.Run();
Console.WriteLine($"Part 1 - accumulator at infinite loop: {handheld.Accumulator}");

for (int i = 0; i < handheld.Memory.Count; i++)
{
    SwapJmpAndNop(i);

    if (handheld.Run())
    {
        Console.WriteLine($"Part 2 - accumulator at end {handheld.Accumulator}");
        return;
    }
    
    SwapJmpAndNop(i);
}

void SwapJmpAndNop(int address)
{
    var operation = handheld.Memory[address].Operation;

    handheld.Memory[address] = operation switch
    {
        Operation.jmp => handheld.Memory[address] with {Operation = Operation.nop},
        Operation.nop => handheld.Memory[address] with {Operation = Operation.jmp},
        _ => handheld.Memory[address]
    };
}