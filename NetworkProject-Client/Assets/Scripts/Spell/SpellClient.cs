using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.Spells;
using NetworkProject.Connection.ToServer;

public class SpellClient : Spell
{
    public SpellClient(Spell spell)
        : base(spell.IdSpell, spell.GetNextUseTime())
    {

    }

    public override void UseSpell(ISpellCaster caster, params ISpellCastOption[] options)
    {
        var request = new UseSpellToServer(IdSpell);

        Client.SendRequestAsMessage(request);

        _nextUseTime = DateTime.UtcNow.AddSeconds(Cooldown);
    }
}
