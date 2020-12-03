// Copyright (c) Six Labors.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;
using System.Numerics;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Drawing.Processing.Processors.Drawing;
using SixLabors.ImageSharp.Drawing.Tests.Processing;
using Xunit;

namespace SixLabors.ImageSharp.Drawing.Tests.Drawing.Paths
{
    public class FillPathCollection : BaseImageOperationsExtensionTest
    {
        private readonly Color color = Color.HotPink;
        private readonly SolidBrush brush = Brushes.Solid(Color.HotPink);
        private readonly IPath path1 = new Path(new LinearLineSegment(
            new PointF[]
            {
                new Vector2(10, 10),
                new Vector2(20, 10),
                new Vector2(20, 10),
                new Vector2(30, 10),
            }));

        private readonly IPath path2 = new Path(new LinearLineSegment(
            new PointF[]
            {
                new Vector2(10, 10),
                new Vector2(20, 10),
                new Vector2(20, 10),
                new Vector2(30, 10),
            }));

        private readonly IPathCollection pathCollection;

        public FillPathCollection()
            => this.pathCollection = new PathCollection(this.path1, this.path2);

        [Fact]
        public void Brush()
        {
            this.operations.Fill(new ShapeGraphicsOptions(), this.brush, this.pathCollection);
            IEnumerable<FillPathProcessor> processors = this.VerifyAll<FillPathProcessor>();

            Assert.All(processors, p =>
            {
                Assert.NotEqual(this.shapeOptions, p.Options.ShapeOptions);
                Assert.Equal(this.brush, p.Brush);
            });

            Assert.Collection(
                processors,
                p => Assert.Equal(this.path1, p.Shape),
                p => Assert.Equal(this.path2, p.Shape));
        }

        [Fact]
        public void BrushWithDefault()
        {
            this.operations.Fill(this.brush, this.pathCollection);
            IEnumerable<FillPathProcessor> processors = this.VerifyAll<FillPathProcessor>();

            Assert.All(processors, p =>
            {
                Assert.Equal(this.shapeOptions, p.Options.ShapeOptions);
                Assert.Equal(this.brush, p.Brush);
            });

            Assert.Collection(
                processors,
                p => Assert.Equal(this.path1, p.Shape),
                p => Assert.Equal(this.path2, p.Shape));
        }

        [Fact]
        public void ColorSet()
        {
            this.operations.Fill(new ShapeGraphicsOptions(), Color.Pink, this.pathCollection);
            IEnumerable<FillPathProcessor> processors = this.VerifyAll<FillPathProcessor>();

            Assert.All(processors, p =>
            {
                Assert.NotEqual(this.shapeOptions, p.Options.ShapeOptions);
                SolidBrush brush = Assert.IsType<SolidBrush>(p.Brush);
                Assert.Equal(Color.Pink, brush.Color);
            });

            Assert.Collection(
                processors,
                p => Assert.Equal(this.path1, p.Shape),
                p => Assert.Equal(this.path2, p.Shape));
        }

        [Fact]
        public void ColorWithDefault()
        {
            this.operations.Fill(Color.Pink, this.pathCollection);
            IEnumerable<FillPathProcessor> processors = this.VerifyAll<FillPathProcessor>();

            Assert.All(processors, p =>
            {
                Assert.Equal(this.shapeOptions, p.Options.ShapeOptions);
                SolidBrush brush = Assert.IsType<SolidBrush>(p.Brush);
                Assert.Equal(Color.Pink, brush.Color);
            });

            Assert.Collection(
                processors,
                p => Assert.Equal(this.path1, p.Shape),
                p => Assert.Equal(this.path2, p.Shape));
        }
    }
}
