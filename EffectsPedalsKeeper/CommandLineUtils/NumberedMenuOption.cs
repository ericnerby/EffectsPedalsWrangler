﻿using System;
using System.Collections.Generic;

namespace EffectsPedalsKeeper.CommandLineUtils
{
    public class NumberedMenuOption<T> : MenuOption
    {
        public IList<T> Items;
        public Action<T> ItemAction;
        public override Action Action => () => ActOnItem(0);
        public NumberedMenuOption(ref IList<T> items, string description, Action<T> itemAction)
            : base(ResponseType.Int, () => { }, "", description)
        {
            Items = items;
            ItemAction = itemAction;
        }

        public bool ActOnItem(int index)
        {
            if (index < 0 || index >= Items.Count)
            {
                return false;
            }
            ItemAction(Items[index]);
            return true;
        }
    }
}