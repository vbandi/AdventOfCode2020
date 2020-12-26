using System;
using System.IO;
using System.Linq;


(int deltaX, int deltaY) direction = (1, 0);
(int x, int y) position = (0, 0);

var input = File.ReadAllLines("input.txt");
var instructions = input.Select(s => new Instruction(Enum.Parse<Operand>(s[0].ToString()), int.Parse(s.Substring(1))));

foreach (var instruction in instructions)
    Execute(instruction);

Console.WriteLine($"Part1 result: {Math.Abs(position.x) + Math.Abs(position.y)}");

position = (0, 0);
direction = (10, -1);

foreach (var instruction in instructions)
    Execute2(instruction);

Console.WriteLine($"Part2 result: {Math.Abs(position.x) + Math.Abs(position.y)}");
    
void Execute(Instruction instruction)
{
    
    switch (instruction.operand)
    {
        case Operand.N:
            position.y -= instruction.value;
            break;
        case Operand.S:
            position.y += instruction.value;
            break;
        case Operand.E:
            position.x += instruction.value;
            break;
        case Operand.W:
            position.x -= instruction.value;
            break;
        case Operand.L when instruction.value == 90:
        case Operand.R when instruction.value == 270:
            var (deltaX, deltaY) = direction;
            direction.deltaX = deltaY;
            direction.deltaY = -deltaX;
            break;
        case Operand.R when instruction.value == 90:
        case Operand.L when instruction.value == 270:
            var (deltaX2, deltaY2) = direction;
            direction.deltaX = -deltaY2;
            direction.deltaY = deltaX2;
            break;
        case Operand.R when instruction.value == 180:
        case Operand.L when instruction.value == 180:
            direction.deltaX *= -1;            
            direction.deltaY *= -1;            
            break;
        case Operand.F:
            position.x += direction.deltaX * instruction.value;
            position.y += direction.deltaY * instruction.value;
            break;
        default:
            throw new ArgumentOutOfRangeException();
    }
}

void Execute2(Instruction instruction)
{
    
    switch (instruction.operand)
    {
        case Operand.N:
            direction.deltaY -= instruction.value;
            break;
        case Operand.S:
            direction.deltaY += instruction.value;
            break;
        case Operand.E:
            direction.deltaX += instruction.value;
            break;
        case Operand.W:
            direction.deltaX -= instruction.value;
            break;
        case Operand.L when instruction.value == 90:
        case Operand.R when instruction.value == 270:
            var (deltaX, deltaY) = direction;
            direction.deltaX = deltaY;
            direction.deltaY = -deltaX;
            break;
        case Operand.R when instruction.value == 90:
        case Operand.L when instruction.value == 270:
            var (deltaX2, deltaY2) = direction;
            direction.deltaX = -deltaY2;
            direction.deltaY = deltaX2;
            break;
        case Operand.R when instruction.value == 180:
        case Operand.L when instruction.value == 180:
            direction.deltaX *= -1;            
            direction.deltaY *= -1;            
            break;
        case Operand.F:
            position.x += direction.deltaX * instruction.value;
            position.y += direction.deltaY * instruction.value;
            break;
        default:
            throw new ArgumentOutOfRangeException();
    }
}

enum Operand
{
    N, S, E, W, L, R, F
}

record Instruction(Operand operand, int value);




