using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject
{
    public struct BreedAndGender
    {
        public Breed Breed
        {
            get
            {
                return _breed;
            }
        }
        public bool IsMale
        {
            get
            {
                return _isMale;
            }
        }

        private Breed _breed;
        private bool _isMale;

        public BreedAndGender(Breed breed, bool isMale)
        {
            _breed = breed;
            _isMale = isMale;
        }
    }
}
