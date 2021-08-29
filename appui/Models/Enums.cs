using System;
namespace appui.Models
{
    public enum Instruction
    {
        WRONG = 0, 
        QUIT = 100,
        PWD = 200,
        LS = 300,
        MKDIR = 400,
        CD = 500,
        TOUCH = 600,
        HELP = 700,
    }
}
