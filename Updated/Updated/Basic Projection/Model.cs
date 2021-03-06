﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using CUE.NET.Devices.Keyboard;
using SRP_3D_Projection_on_Keyboard.Different_Platforms.WindowsForm;
using SRP_3D_Projection_on_Keyboard.Different_Platforms.Console;

namespace SRP_3D_Projection_on_Keyboard.Basic_Projection {
    public class Model {
        private PointF3D[] vertexes;
        private PolyLine[] faces;
        private PolyLine[] polyLines;

        private PointF3D rotation;
        private PointF3D translator;
        private PointF3D scaler;
        private PointF translator2D;

        private PointF[] calc2DPoints;

        public ModelProps properties = new ModelProps();

        //Constructor - Main
        public Model(PointF3D[] vertexes) {
            Setup();
            this.vertexes = vertexes;
            calc2DPoints = new PointF[vertexes.Length];
            CalcNewPoints();
        }

        //Constructor - With lines to draw between
        public Model(PointF3D[] vertexes, PolyLine[] polyLines) {
            Setup();
            this.vertexes = vertexes;
            this.polyLines = polyLines;
            calc2DPoints = new PointF[vertexes.Length];
            CalcNewPoints();
        }

        //Constructor - With lines to draw between and faces
        public Model(PointF3D[] vertexes, PolyLine[] polyLines, PolyLine[] faces) {
            Setup();
            this.vertexes = vertexes;
            this.polyLines = polyLines;
            this.faces = faces;
            calc2DPoints = new PointF[vertexes.Length];
            CalcNewPoints();
        }

        //Constructor - With rotation
        public Model(PointF3D[] vertexes, PointF3D rotation) {
            Setup();
            this.vertexes = vertexes;
            this.rotation = rotation;
            calc2DPoints = new PointF[vertexes.Length];
            CalcNewPoints();
        }

        private void Setup() {
            rotation = new PointF3D();
            translator = new PointF3D();
            scaler = new PointF3D(1, 1, 1);
            translator2D = new PointF();
            faces = new PolyLine[0];
            polyLines = new PolyLine[0];
    }

        public void Draw(CorsairKeyboard keyboard, Color colorPoints, Color colorLines) {
            //Tegner rotations midt
            if (properties.displayCenterOfRotation) {
                Different_Platforms.CUE.Draw.LEDDrawAt(keyboard, Color.Blue, CalcNewPoint(new PointF3D())); 
            }
            
            //Draw lines
            DrawLines(keyboard, colorLines);

            //Draw with points on them
            DrawPoints(keyboard, colorPoints);
        }

        //Her tegner vi et wireframe
        private void DrawLines(CorsairKeyboard keyboard, Color color) {
            if (properties.displayLines) {
                foreach (var polyLine in polyLines) {
                    //Loop through polyline
                    for (int i = 0; i < polyLine.indexesInModel.Length - 1; i++) {
                        //Draw line
                        Different_Platforms.CUE.Draw.LEDDrawLineAt(keyboard, color, calc2DPoints[polyLine.indexesInModel[i]], calc2DPoints[polyLine.indexesInModel[i + 1]]);
                    }
                } 
            }

            if (properties.displayFaces) {
                foreach (var face in faces) {
                    //Loop through polyline in face
                    for (int i = 0; i < face.indexesInModel.Length - 1; i++) {
                        //Draw line
                        Different_Platforms.CUE.Draw.LEDDrawLineAt(keyboard, color, calc2DPoints[face.indexesInModel[i]], calc2DPoints[face.indexesInModel[i + 1]]);
                    }

                    //Draw last line
                    Different_Platforms.CUE.Draw.LEDDrawLineAt(keyboard, color, calc2DPoints[face.indexesInModel[0]], calc2DPoints[face.indexesInModel[face.indexesInModel.Length - 1]]);
                } 
            }
        }

        private void DrawPoints(CorsairKeyboard keyboard, Color color) {
            if (properties.displayPoints) {
                Different_Platforms.CUE.Draw.LEDDrawAt(keyboard, color, calc2DPoints); 
            }
        }

        public void Draw(WindowsForms canvas, Color colorPoints, Color colorLines) {
            //Tegner rotations midt
            if (properties.displayCenterOfRotation) {
                var center = CalcNewPoint(new PointF3D());
                canvas.DrawAt(Color.Blue, (int)center.X, (int)center.Y, properties.wFPointSize * 4f); 
            }

            //Draw lines
            DrawLines(canvas, colorLines);

            //Draw with points on them
            DrawPoints(canvas, colorPoints);
        }

        //Her tegner vi et wireframe
        private void DrawLines(WindowsForms canvas, Color color) {
            if (properties.displayLines) {
                foreach (var polyLine in polyLines) {
                    //Loop through polyline
                    for (int i = 0; i < polyLine.indexesInModel.Length - 1; i++) {
                        //Draw line
                        canvas.DrawLineAt(color, calc2DPoints[polyLine.indexesInModel[i]], calc2DPoints[polyLine.indexesInModel[i + 1]]);
                    }
                }
            }

            if (properties.displayFaces) {
                foreach (var face in faces) {
                    //Loop through polyline in face
                    for (int i = 0; i < face.indexesInModel.Length - 1; i++) {
                        //Draw line
                        canvas.DrawLineAt(color, calc2DPoints[face.indexesInModel[i]], calc2DPoints[face.indexesInModel[i + 1]]);
                    }

                    //Draw last line
                    canvas.DrawLineAt(color, calc2DPoints[face.indexesInModel[0]], calc2DPoints[face.indexesInModel[face.indexesInModel.Length - 1]]);
                }
            }
        }

        private void DrawPoints(WindowsForms canvas, Color color) {
            if (properties.displayPoints) {
                canvas.DrawAt(color, calc2DPoints); 
            }
        }

        public void Draw(Draw canvas, ConsoleColor colorPoints, ConsoleColor colorLines) {
            //Tegner rotations midt
            if (properties.displayCenterOfRotation) {
                canvas.DrawAt(ConsoleColor.Blue, CalcNewPoint(new PointF3D())); 
            }
            
            //Tegner linjer foerst
            DrawLines(canvas, colorLines);

            //Derefter punkter
            DrawPoints(canvas, colorPoints);
        }

        //Her tegner vi et wireframe
        private void DrawLines(Draw canvas, ConsoleColor color) {
            if (properties.displayLines) {
                foreach (var polyLine in polyLines) {
                    //Loop through polyline
                    for (int i = 0; i < polyLine.indexesInModel.Length - 1; i++) {
                        //Draw line
                        canvas.DrawLineAt(color, calc2DPoints[polyLine.indexesInModel[i]], calc2DPoints[polyLine.indexesInModel[i + 1]]);
                    }
                }
            }

            if (properties.displayFaces) {
                foreach (var face in faces) {
                    //Loop through polyline in face
                    for (int i = 0; i < face.indexesInModel.Length - 1; i++) {
                        //Draw line
                        canvas.DrawLineAt(color, calc2DPoints[face.indexesInModel[i]], calc2DPoints[face.indexesInModel[i + 1]]);
                    }

                    //Draw last line
                    canvas.DrawLineAt(color, calc2DPoints[face.indexesInModel[0]], calc2DPoints[face.indexesInModel[face.indexesInModel.Length - 1]]); 
                }
            }
        }

        private void DrawPoints(Draw canvas, ConsoleColor color) {
            if (properties.displayPoints) {
                canvas.DrawAt(color, calc2DPoints); 
            }
        }

        public PointF CalcNewPoint(PointF3D vertex) {
            //Scale the vertex
            var Vertex = vertex * scaler;

            //Translate it
            Vertex = Vertex + translator;

            //Rotate it
            Vertex = Matrix.Rotate(rotation, Vertex);

            //Project it
            var point = Matrix.Project(Vertex);

            //Translate it
            return new PointF(point.X + translator2D.X, point.Y + translator2D.Y);
        }

        private void CalcNewPoints() {
            //For each vertex in model
            for (int i = 0; i < vertexes.Length; i++) {
                //Add it to calc2DPoints
                calc2DPoints[i] = CalcNewPoint(vertexes[i]);
            }
        }

        public void Rotation(PointF3D rotation) {
            //Edit rotation
            this.rotation = rotation;

            //Calculate new points
            CalcNewPoints();
        }

        public void RotateBy(PointF3D rotateBy) {
            //Edit rotater
            rotation += rotateBy;

            //Calculate new points
            CalcNewPoints();
        }

        public void Translation(PointF3D translation) {
            //Edit translater
            this.translator = translation;

            //Calculate new points
            CalcNewPoints();
        }

        public void TranslateBy(PointF3D translateBy) {
            //Edit translater
            translator += translateBy;

            //Calculate new points
            CalcNewPoints();
        }

        public void Scaler(PointF3D scaler) {
            //Edit translater
            this.scaler = scaler;

            //Calculate new points
            CalcNewPoints();
        }

        public void Scaler(float scaler) {
            //Edit translater
            this.scaler = new PointF3D(scaler, scaler, scaler);

            //Calculate new points
            CalcNewPoints();
        }

        public void ScaleBy(float scaleBy) {
            //Edit translater
            scaler += scaleBy;

            //Calculate new points
            CalcNewPoints();
        }

        public void ScaleBy(PointF3D scaleBy) {
            //Edit translater
            scaler += scaleBy;

            //Calculate new points
            CalcNewPoints();
        }

        public void Translation(PointF translation) {
            //Edit translater
            this.translator2D = translation;

            //Calculate new points
            CalcNewPoints();
        }

        public void TranslateBy(PointF translateBy) {
            //Edit translater
            translator2D.X += translateBy.X;
            translator2D.Y += translateBy.Y;

            //Calculate new points
            CalcNewPoints();
        }

        public PointF3D GetRotation() {
            return rotation;
        }

        public PointF3D GetTranslater() {
            return translator;
        }

        public PointF3D GetScaler() {
            return scaler;
        }

        public PointF GetTranslater2D() {
            return translator2D;
        }

        public PointF[] GetPoints() {
            return calc2DPoints;
        }
    }
}