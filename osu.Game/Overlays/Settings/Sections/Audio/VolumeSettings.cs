﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Audio;
using osu.Framework.Graphics;
using osu.Framework.Localisation;
using osu.Game.Configuration;
using osu.Game.Graphics.UserInterface;
using osu.Game.Localisation;

namespace osu.Game.Overlays.Settings.Sections.Audio
{
    public partial class VolumeSettings : SettingsSubsection
    {
        protected override LocalisableString Header => AudioSettingsStrings.VolumeHeader;

        private readonly Decibel volumeInactive = new Decibel();

        [BackgroundDependencyLoader]
        private void load(AudioManager audio, OsuConfigManager config)
        {
            config.BindWith(OsuSetting.VolumeInactive, volumeInactive.Real);
            volumeInactive.UpdateScale();

            Children = new Drawable[]
            {
                new VolumeAdjustSlider
                {
                    LabelText = AudioSettingsStrings.MasterVolume,
                    Current = audio.Volume.Scale,
                    KeyboardStep = (float)Decibel.STEP,
                },
                new VolumeAdjustSlider
                {
                    LabelText = AudioSettingsStrings.MasterVolumeInactive,
                    Current = volumeInactive.Scale,
                    KeyboardStep = (float)Decibel.STEP,
                    PlaySamplesOnAdjust = true,
                },
                new VolumeAdjustSlider
                {
                    LabelText = AudioSettingsStrings.EffectVolume,
                    Current = audio.VolumeSample.Scale,
                    KeyboardStep = (float)Decibel.STEP,
                },

                new VolumeAdjustSlider
                {
                    LabelText = AudioSettingsStrings.MusicVolume,
                    Current = audio.VolumeTrack.Scale,
                    KeyboardStep = (float)Decibel.STEP,
                },
            };
        }

        private partial class DecibelSliderBar : RoundedSliderBar<double>
        {
            public override LocalisableString TooltipText => (Current.Value <= Decibel.MIN ? "-∞" : Current.Value.ToString("+#0.0;-#0.0;+0.0")) + " dB";
        }

        private partial class VolumeAdjustSlider : SettingsSlider<double>
        {
            protected override Drawable CreateControl() => new DecibelSliderBar
            {
                RelativeSizeAxes = Axes.X,
                PlaySamplesOnAdjust = false,
            };

            public bool PlaySamplesOnAdjust
            {
                get => ((DecibelSliderBar)Control).PlaySamplesOnAdjust;
                set => ((DecibelSliderBar)Control).PlaySamplesOnAdjust = value;
            }
        }
    }
}
