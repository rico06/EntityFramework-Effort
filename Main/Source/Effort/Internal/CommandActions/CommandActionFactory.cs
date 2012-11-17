﻿// --------------------------------------------------------------------------------------------
// <copyright file="CommandActionFactory.cs" company="Effort Team">
//     Copyright (C) 2012 Effort Team
//
//     Permission is hereby granted, free of charge, to any person obtaining a copy
//     of this software and associated documentation files (the "Software"), to deal
//     in the Software without restriction, including without limitation the rights
//     to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//     copies of the Software, and to permit persons to whom the Software is
//     furnished to do so, subject to the following conditions:
//
//     The above copyright notice and this permission notice shall be included in
//     all copies or substantial portions of the Software.
//
//     THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//     IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//     FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//     AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//     LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//     OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//     THE SOFTWARE.
// </copyright>
// --------------------------------------------------------------------------------------------

namespace Effort.Internal.CommandActions
{
    using System;
    using System.Data.Common.CommandTrees;

    internal static class CommandActionFactory
    {
        public static ICommandAction Create(DbCommandTree commandTree)
        {
            ICommandAction action = null;

            if (commandTree is DbQueryCommandTree)
            {
                action = new QueryCommandAction(commandTree as DbQueryCommandTree);
            }
            else if (commandTree is DbInsertCommandTree)
            {
                action = new InsertCommandAction(commandTree as DbInsertCommandTree);
            }
            else if (commandTree is DbUpdateCommandTree)
            {
                action = new UpdateCommandAction(commandTree as DbUpdateCommandTree);
            }
            else if (commandTree is DbDeleteCommandTree)
            {
                action = new DeleteCommandAction(commandTree as DbDeleteCommandTree);
            }

            if (action == null)
            {
                throw new NotSupportedException("Not supported DbCommandTree type");
            }

            return action;
        }
    }
}