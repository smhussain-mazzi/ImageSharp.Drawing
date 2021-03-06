// Copyright (c) Six Labors.
// Licensed under the Apache License, Version 2.0.

using System;

namespace SixLabors.ImageSharp.Drawing
{
    /// <summary>
    /// Represents a region of an image.
    /// </summary>
    public abstract class Region
    {
        /// <summary>
        /// Gets the bounding box that entirely surrounds this region.
        /// </summary>
        public abstract Rectangle Bounds { get; }

        // We should consider removing Region, so keeping this internal for now.
        internal abstract IPath Shape { get; }
    }
}
