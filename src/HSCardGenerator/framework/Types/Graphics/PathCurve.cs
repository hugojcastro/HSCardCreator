/*
 * This file is part of HSCardGenerator.
 *
 * Licensed under the MIT license. See LICENSE file in the project root for full license information.
 *
 * HSCardGenerator is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
 * warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 *
 * Hearthstone®: Heroes of Warcraft
 * ©2014 Blizzard Entertainment, Inc. All rights reserved. Heroes of Warcraft is a trademark, and Hearthstone is a registered trademark of Blizzard Entertainment, Inc. in the U.S. and/or other countries.
 *
 * HTMLRenderer is a library originally created by Jose Menendez Póo and maintained by ArthurHub. More information and license in: https://github.com/ArthurHub/HTML-Renderer
 *
 * Newtonsoft.Json is open source licensed to James Newton-King under the MIT license. More information and license in: https://github.com/JamesNK/Newtonsoft.Json
 */

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

using HSCardGenerator.framework.Constants.Common;
using HSCardGenerator.framework.Constants.Graphics;

namespace HSCardGenerator.framework.Types.Graphics
{
    /// <summary>
    /// Class to create and draw texts over curves.
    /// Atm, only 3 types of curves are implemented:
    /// Linear curve: from one point to other, text in line with it (any direction)
    /// Quadratic curve: using 3 points, it's like an 'U'
    /// Cubic curve: using 4 points, it's like a rotated 'S'
    /// </summary>
    public class PathCurve
    {
        /// <summary>
        /// Define properties for the class. Mainly, factors for curves, type and reference points
        /// </summary>
        #region Properties
        // Store curve type
        private CurveType type = CurveType.None;
        // Factors for cubic equation:     y = a·x^3 + b·x^2 + c·x + d
        // Factors for quadratic equation: y = a·x^2 + b·x + c
        // Factors for linear equation:    y = a·x + b
        private float a = 0, b = 0, c = 0, d = 0;
        // Keep first and last points
        private PointF first = Point.Empty, last = Point.Empty;
        // Segments to divide paths
        private static int pathSegments = 30;
        #endregion

        /// <summary>
        /// Constructor of PathCurve class.
        /// Get the number of points passed as argument and calculate corresponding curve.
        /// </summary>
        /// <param name="p">Array with points for the curve (2, 3 or 4)</param>
        public PathCurve(PointF[] p)
        {
            // define common var
            var det = 0f;
            // Depending on number of points from arg array
            switch (p.Length)
            {
                case 2: // Linear: NOT VALID FOR VERTICAL LINES! (det = 0 :P)
                    // Let's use Cramer: get determinant for 2x2 matrix
                    det = p[0].X - p[1].X;
                    // Calculate determinants for 3x3 matrix from column substitution and divide them by previous determinant
                    a = (p[0].Y - p[1].Y) / det;
                    b = (p[0].X * p[1].Y - p[0].Y * p[1].X) / det;
                    // Store first and last points
                    first = new PointF(p[0].X, p[0].Y);
                    last  = new PointF(p[1].X, p[1].Y);
                    // Store type
                    type = CurveType.Linear;
                    break;
                case 3: // Quadratic
                    // Let's use Cramer: get determinant for 3x3 matrix
                    det = p[0].X * p[0].X * (p[1].X - p[2].X) + p[1].X * p[1].X * (p[2].X - p[0].X) + p[2].X * p[2].X * (p[0].X - p[1].X);
                    // Calculate determinants for 3x3 matrix from column substitution and divide them by previous determinant
                    a = (p[0].Y * (p[1].X - p[2].X) + p[1].Y * (p[2].X - p[0].X) + p[2].Y * (p[0].X - p[1].X)) / det;
                    b = (p[0].X * p[0].X * (p[1].Y - p[2].Y) + p[1].X * p[1].X * (p[2].Y - p[0].Y) + p[2].X * p[2].X * (p[0].Y - p[1].Y)) / det;
                    c = (p[0].X * p[0].X * (p[1].X * p[2].Y - p[2].X * p[1].Y) + p[1].X * p[1].X * (p[2].X * p[0].Y - p[2].Y * p[0].X) + p[2].X * p[2].X * (p[0].X * p[1].Y - p[1].X * p[0].Y)) / det;
                    // Store first and last points
                    first = new PointF(p[0].X, p[0].Y);
                    last  = new PointF(p[2].X, p[2].Y);
                    // Store type
                    type = CurveType.Quadratic;
                    break;
                case 4: // Cubic
                    // Let's use Gauss-Jordan to get the curve parameters
                    // Create vectors for linear equation system
                    var system = new float[5, 4];
                    for (var lin = 0; lin < 4; lin++)
                    {
                        system[0, lin] = p[lin].X * p[lin].X * p[lin].X;
                        system[1, lin] = p[lin].X * p[lin].X;
                        system[2, lin] = p[lin].X;
                        system[3, lin] = 1;
                        system[4, lin] = p[lin].Y;
                    }
                    // get 1's on diagonal and 0's under them
                    for (var loop = 0; loop < 4; loop++)
                    {
                        // Get 1 in (loop, loop) position
                        // Check if (loop, loop) = 0; if so, fix it
                        if (system[loop, loop] == 0)
                            for (var col = 0; col < 5; col++) system[col, loop] = system[col, 0] + system[col, 1] + system[col, 2] + system[col, 3];
                        // Divide all line by itself to get (loop, loop) = 1
                        var factor = system[loop, loop];
                        for (var col = 0; col < 5; col++) system[col, loop] = system[col, loop] / factor;
                        // Get 0 below diagonal
                        for (var lin = loop + 1; lin < 4; lin++)
                        {
                            factor = system[loop, lin];
                            for (var col = loop; col < 5; col++) system[col, lin] = system[col, lin] - system[col, loop] * factor;
                        }
                    }
                    // Get 0's over diagonal elements
                    for (var loop = 3; loop >= 0; loop--)
                    {
                        for (var lin = loop - 1; lin >= 0; lin--)
                        {
                            var factor = system[loop, lin];
                            for (var col = loop; col < 5; col++) system[col, lin] = system[col, lin] - system[col, loop] * factor;
                        }
                    }
                    // Get parameter values
                    a = system[4, 0];
                    b = system[4, 1];
                    c = system[4, 2];
                    d = system[4, 3];
                    // Keep first and last points
                    first = new PointF(p[0].X, p[0].Y);
                    last  = new PointF(p[3].X, p[3].Y);
                    // Store type
                    type = CurveType.Cubic;
                    break;
                default:
                    throw new System.ArgumentException("Invalid number of points for array! (allowed: 2 to 4)", "p");
            }
        }

        /// <summary>
        /// Get then Y coord value for curve from a X value
        /// </summary>
        /// <param name="x">Value to calculate Y from.</param>
        /// <returns></returns>
        public float getY(float x)
        {
            // initialize value
            float result = 0f;
            // get right value based on curve type
            switch (type)
            {
                case CurveType.Linear:
                    result = a * x + b;
                    break;
                case CurveType.Quadratic:
                    result = (float)(a * Math.Pow(x, 2) + b * x + c);
                    break;
                case CurveType.Cubic:
                    result = (float)(a * Math.Pow(x, 3) + b * Math.Pow(x, 2) + c * x + d);
                    break;
                default:
                    throw new System.ArgumentException("Unknown curve type!", "type");
            }
            // return value
            return result;
        }

        /// <summary>
        /// Get a path of 'count' points from the curve.
        /// Giving a number of points, method returns an array of points from First to Last, contained in the curve.
        /// </summary>
        /// <param name="count">Number of points to return</param>
        /// <returns></returns>
        public PointF[] getPath(int count)
        {
            // Create result array
            PointF[] result = new PointF[count];
            // Calculate delta (curve divided in equal segments)
            var delta = (last.X - first.X) / (count - 1);
            // Calculate points using first point as reference
            for (var idx = 0; idx < count; idx++)
            {
                result[idx].X = first.X + delta * idx;
                result[idx].Y = getY(result[idx].X);
            }
            // return point array
            return result;
        }

        /// <summary>
        /// Get length for a path of 'count' points for the curve.
        /// It uses aproximation as the sum of chords of the curve as divided in -count- arcs.
        /// </summary>
        /// <param name="count">Number of arcs to divide curve. Length will be the sum or their chords. The more, the better.</param>
        /// <returns></returns>
        public float getPathLength(int count)
        {
            // Create result array
            PointF[] p = new PointF[count];
            // Calculate delta (curve divided in equal segments)
            var delta = (last.X - first.X) / (count - 1);
            // Calculate points using first point as reference
            for (var idx = 0; idx < count; idx++)
            {
                p[idx].X = first.X + delta * idx;
                p[idx].Y = getY(p[idx].X);
            }
            // Get length: sum all distances
            double length = 0;
            for (var idx = count - 1; idx > 0; idx--)
            {
                length += Math.Sqrt(Math.Pow(p[idx].X - p[idx - 1].X, 2) + Math.Pow(p[idx].Y - p[idx - 1].Y, 2));
            }
            // return it
            return (float)length;
        }

        /// <summary>
        /// Render string text in graphics over a path using given font.
        /// Path will be a cuve (the one defined in main class).
        /// </summary>
        /// <param name="g">Graphic to draw to.</param>
        /// <param name="text">Text to draw.</param>
        /// <param name="component">CardGraphicComponent to get style parameters to draw text (font, size...)</param>
        /// <param name="squish">Used to adjust spaces between chars due to MeasureString</param>
        /// <param name="debug">If true, it draws a red curve as reference. Default: do not draw.</param>
        public void renderText(System.Drawing.Graphics g, string text, CardGraphicComponent component, bool squish = false, bool debug = false)
        {
            // Define vars
            Font fontR  = Graphic.findBestFitFont(g, text, component.font, getPathLength(pathSegments), squish);
            var width   = Graphic.getStringWidth(g, text, fontR, squish);
            // If text too big and it was adjusted, we need adjust the vertical offset too
            var adjust  = component.font.Size - fontR.Size;
            var middleX = (last.X + first.X) / 2;
            var middleY = getY(middleX);
            var startX  = middleX - (width / 2);
            var startY = getY(startX);
            var len     = text.Length;
            var space   = g.MeasureString(" ", fontR, Point.Empty, StringFormat.GenericDefault).Width;
            var factor  = (squish) ? Config.align : 0f;
            var flaw    = 2 * fontR.Size / component.font.Size; // First char needs an offset, idk why :S
            float w;
            // set atialiasing
            g.SmoothingMode     = SmoothingMode.HighQuality; // AntiAlias
            g.InterpolationMode = InterpolationMode.HighQualityBicubic; // High
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            g.PixelOffsetMode   = PixelOffsetMode.HighQuality;
            // Go along string and render chars
            for (int characterIndex = 0; characterIndex < len; characterIndex++)
            {
                char @char = text[characterIndex];
                w = (@char == ' ') ? space : g.MeasureString(@char.ToString(), fontR, Point.Empty, StringFormat.GenericTypographic).Width - factor;
                // put char in (startX, startY)
                using (GraphicsPath characterPath = new GraphicsPath())
                {
                    characterPath.AddString(@char.ToString(), fontR.FontFamily, (int)fontR.Style, fontR.Size, Point.Empty, StringFormat.GenericTypographic);
                    // Transformation matrix to move the character to the correct location. 
                    // Note that all actions on the Matrix class are prepended, so we apply them in reverse.
                    var transform = new Matrix();
                    // Translate to the final position
                    transform.Translate(startX + flaw, startY + adjust);
                    // Rotate the character
                    var cathetus = getY(startX + w) - startY;
                    var hypothenuse = Math.Sqrt(Math.Pow(w - flaw, 2) + Math.Pow(cathetus, 2));
                    var angle = (factor == 0) ? 0f : Math.Asin(cathetus / hypothenuse) * 180f / (float)Math.PI;
                    transform.Rotate((float)angle);
                    // Apply transformations
                    characterPath.Transform(transform);
                    // Draw the character
                    using (Pen pen = new Pen(component.borderColor, component.Outline))
                    {
                        pen.LineJoin = LineJoin.Round;

                        g.DrawPath(pen, characterPath);
                        g.FillPath(component.textColor, characterPath);
                    }
                }
                // Skip offset for the other chars
                flaw = 0;
                // Update new position
                startX += w;
                startY = getY(startX);
            }
            // Show path line if debugging
            if (debug)
            {
                // Get path of N points
                PointF[] points = getPath(pathSegments);
                using (GraphicsPath path = new GraphicsPath())
                {
                    // Create curve
                    path.AddCurve(points);
                    // And draw it to the screen.
                    using (Pen pen = new Pen(Color.Red, 2))
                    {
                        g.DrawPath(pen, path);
                    }
                }
            }
        }
    }
}
