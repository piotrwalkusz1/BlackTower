﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject
{
    [Serializable]
    public struct BreedAndGender
    {
        public int Breed
        {
            get { return _breed; }               
            set { _breed = value; }
        }
        public bool IsMale
        {
            get { return _isMale; }
            set { _isMale = value; }
        }

        private int _breed;
        private bool _isMale;

        public BreedAndGender(int breed, bool isMale)
        {
            _breed = breed;
            _isMale = isMale;
        }
    }
}
