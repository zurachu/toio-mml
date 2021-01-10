using System;
using System.Collections.Generic;
using TextPlayer;
using TextPlayer.MML;
using toio;

public class ToioCubeMmlPlayer : MMLPlayer
{
    private static Dictionary<char, int> noteMap = new Dictionary<char, int>
    {
        {'c', (int) Cube.NOTE_NUMBER.C0},
        {'d', (int) Cube.NOTE_NUMBER.D0},
        {'e', (int) Cube.NOTE_NUMBER.E0},
        {'f', (int) Cube.NOTE_NUMBER.F0},
        {'g', (int) Cube.NOTE_NUMBER.G0},
        {'a', (int) Cube.NOTE_NUMBER.A0},
        {'b', (int) Cube.NOTE_NUMBER.B0},
    };
    private static int noteNumberPerOctave = Cube.NOTE_NUMBER.C1 - Cube.NOTE_NUMBER.C0;

    private readonly Cube cube;

    public ToioCubeMmlPlayer(Cube cube) : base()
    {
        this.cube = cube;
    }

    protected override void PlayNote(Note note, int channel, TimeSpan time)
    {
        var durationMs = (int)note.Length.TotalMilliseconds;
        var volume = (byte)(note.Volume * 255);
        var noteNumber = (byte)(noteMap[note.Type] + note.Octave * noteNumberPerOctave);
        if (note.Sharp)
        {
            noteNumber++;
        }

        var operations = new Cube.SoundOperation[]
        {
            new Cube.SoundOperation(durationMs: durationMs, volume: volume, note_number: noteNumber)
        };

        cube.PlaySound(1, operations);
    }
}
