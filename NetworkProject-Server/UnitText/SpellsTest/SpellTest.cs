using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetworkProject;
using Moq;

namespace UnitTest.SpellsTest
{
    [TestClass]
    public class SpellTest
    {
        [TestMethod]
        public void CanUseSpell_LvlRequirement_Success()
        {
            SpellRequirement lvlRequirement = new SpellRequirement(4, SpellRequirementType.Lvl);
            SpellData spellData = new SpellData(23, lvlRequirement);
            Spell spell = new Spell(spellData);
            var mockSpellCaster = new Mock<ISpellCaster>();
            mockSpellCaster.SetupGet(m => m.Lvl).Returns(4);
            var caster = mockSpellCaster.Object;

            bool success = spell.CanUseSpell(caster);

            Assert.AreEqual(true, success);
        }

        [TestMethod]
        public void CanUseSpell_LvlRequirement_Fail()
        {
            SpellRequirement lvlRequirement = new SpellRequirement(4, SpellRequirementType.Lvl);
            SpellData spellData = new SpellData(23, lvlRequirement);
            Spell spell = new Spell(spellData);
            var mockSpellCaster = new Mock<ISpellCaster>();
            mockSpellCaster.SetupGet(m => m.Lvl).Returns(3);
            var caster = mockSpellCaster.Object;

            bool success = spell.CanUseSpell(caster);

            Assert.AreEqual(false, success);
        }

        [TestMethod]
        public void CanUseSpell_ManaRequirement_Success()
        {
            SpellRequirement manaRequirement = new SpellRequirement(20, SpellRequirementType.Mana);
            SpellData spellData = new SpellData(23, manaRequirement);
            Spell spell = new Spell(spellData);
            var mockSpellCaster = new Mock<ISpellCaster>();
            mockSpellCaster.SetupGet(m => m.Mana).Returns(20);
            var caster = mockSpellCaster.Object;

            bool success = spell.CanUseSpell(caster);

            Assert.AreEqual(true, success);
        }

        [TestMethod]
        public void CanUseSpell_ManaRequirement_Fail()
        {
            SpellRequirement manaRequirement = new SpellRequirement(20, SpellRequirementType.Mana);
            SpellData spellData = new SpellData(23, manaRequirement);
            Spell spell = new Spell(spellData);
            var mockSpellCaster = new Mock<ISpellCaster>();
            mockSpellCaster.SetupGet(m => m.Mana).Returns(19);
            var caster = mockSpellCaster.Object;

            bool success = spell.CanUseSpell(caster);

            Assert.AreEqual(false, success);
        }

        [TestMethod]
        public void CanUseSpell_TimeRequirement()
        {
            int a;
            SpellFunction func = (x, y) => { };
            SpellRequirement timeRequirement = new SpellRequirement(1000f, SpellRequirementType.TimeCooldown);
            SpellData spellData = new SpellData(1, func, timeRequirement);
            Spell spell = new Spell(spellData);
            var mockSpellCaster = new Mock<ISpellCaster>();
            var caster = mockSpellCaster.Object;

            bool success1 = spell.UseSpell(caster);
            bool success2 = spell.UseSpell(caster);

            Assert.AreEqual(true, success1);
            Assert.AreEqual(false, success2);
        }
    }
}
