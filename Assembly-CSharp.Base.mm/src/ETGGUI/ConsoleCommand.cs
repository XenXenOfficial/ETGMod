﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


/// <summary>
/// ETG Mod console command class
/// </summary>
public class ConsoleCommand {


    /// <summary>
    /// The command's name in the console, what you use to call it.
    /// </summary>
    public string CommandName;

    /// <summary>
    /// The function run when
    /// </summary>
    public System.Action<string[]> CommandReference;

    /// <summary>
    /// A 2D array of all accepted arguments.
    /// The first coordinate is the argument number, returned array is a list of accepted arguments.
    /// </summary>
    public string[][] AcceptedArguments;

    public ConsoleCommand(string cmdName, System.Action<string[]> cmdRef, params string[][] acceptedArgs) {
        CommandName = cmdName;
        CommandReference = cmdRef;
        if (acceptedArgs != null) {
            AcceptedArguments = acceptedArgs;
        }
    }

    public void RunCommand(string[] args) {
        if (AcceptedArguments!=null) {
            for (int i = 0; i<args.Length; i++) {

                //Out of range, don't bother anymore
                if (AcceptedArguments.Length<=i) {
                    break;
                }

                //There's no acceptable arguments, so skip this.
                if (AcceptedArguments[i]==null) {
                    continue;
                }

                //Did we find a matching command in the acceptable arguments?
                bool foundMatch=false;

                foreach (string s in AcceptedArguments[i]) {
                    if (s==args[i]) {
                        foundMatch=true;
                        break;
                    }
                }

                if (foundMatch) {
                    continue;
                } else {
                    Debug.Log("Unnaceptable argument " +'"'+args[i]+'"' +" in command " + CommandName);
                    return;
                }
            }
        }

        CommandReference(args);
    }

    internal bool IsCommandCorrect(string[] splitCommand) {

        if (AcceptedArguments==null)
            return true;

        for(int i = 1; i < splitCommand.Length; i++) {

            if (AcceptedArguments.Length<=i-1)
                return true;

            if (AcceptedArguments[i-1]==null)
                continue;

            bool isContained = false;
            for(int j = 0; j < AcceptedArguments[i-1].Length && !isContained; j++) {
                if (AcceptedArguments[i-1][j]==splitCommand[i])
                    isContained=true;
            }
            if (!isContained)
                return false;
        }

        return true;
    }
}

