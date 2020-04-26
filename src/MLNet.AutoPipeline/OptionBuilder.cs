﻿// <copyright file="OptionBuilder.cs" company="BigMiao">
// Copyright (c) BigMiao. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using MLNet.Sweeper;

namespace MLNet.AutoPipeline
{
    public abstract class OptionBuilder<TOption>
        where TOption : class
    {
        private readonly HashSet<string> _groupIDs = new HashSet<string>();

        public IValueGenerator[] ValueGenerators => this.GetParameterAttributes().Select(kv => kv.Value.ValueGenerator).ToArray();

        public TOption CreateDefaultOption()
        {
            var assem = typeof(TOption).Assembly;
            var option = assem.CreateInstance(typeof(TOption).FullName) as TOption;

            // set up fields
            var fields = this.GetType().GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            foreach (var field in fields)
            {
                var value = field.GetValue(this);
                option.GetType().GetField(field.Name)?.SetValue(option, value);
            }

            return option;
        }

        public TOption BuildOption(ParameterSet parameters)
        {
            var option = this.CreateDefaultOption();
            foreach (var param in parameters)
            {
                if (this._groupIDs.Contains(param.GroupID))
                {
                    var value = param.RawValue;
                    typeof(TOption).GetField(param.Name)?.SetValue(option, value);
                }
            }

            return option;
        }

        private Dictionary<string, ParameterAttribute> GetParameterAttributes()
        {
            var paramaters = this.GetType().GetFields()
                     .Where(x => Attribute.GetCustomAttribute(x, typeof(ParameterAttribute)) != null);

            var paramatersDictionary = new Dictionary<string, ParameterAttribute>();

            foreach (var param in paramaters)
            {
                var paramaterAttribute = Attribute.GetCustomAttribute(param, typeof(ParameterAttribute)) as ParameterAttribute;
                this._groupIDs.Add(paramaterAttribute.GroupID);
                paramatersDictionary.Add(param.Name, paramaterAttribute);
            }

            return paramatersDictionary;
        }
    }
}