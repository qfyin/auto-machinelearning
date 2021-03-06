﻿// <copyright file="Utils.cs" company="BigMiao">
// Copyright (c) BigMiao. All rights reserved.
// </copyright>

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Formatting;
using System;
using System.Collections.Generic;
using System.Text;

namespace MLNet.CodeGenerator.Tests
{
    internal static class Utils
    {
        internal static string FormatCode(string trainProgramCSFileContent)
        {
            // Format code

            var tree = CSharpSyntaxTree.ParseText(trainProgramCSFileContent);
            var syntaxNode = tree.GetRoot();
            trainProgramCSFileContent = Formatter.Format(syntaxNode, new AdhocWorkspace()).ToFullString();
            return trainProgramCSFileContent;
        }
    }
}
