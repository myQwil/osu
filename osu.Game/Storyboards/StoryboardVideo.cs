﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Graphics;
using osu.Game.Storyboards.Drawables;
using osuTK;

namespace osu.Game.Storyboards
{
    public class StoryboardVideo : StoryboardSprite
    {
        public StoryboardVideo(string path, double offset)
            : base(path, Anchor.Centre, Vector2.Zero)
        {
            TimelineGroup.Alpha.Add(Easing.None, offset, offset, 0, 1);
        }

        public override Drawable CreateDrawable() => new DrawableStoryboardVideo(this);
    }
}
