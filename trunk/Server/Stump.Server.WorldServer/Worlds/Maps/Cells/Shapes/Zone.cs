using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Database.World;

namespace Stump.Server.WorldServer.Worlds.Maps.Cells.Shapes
{
    public class Zone : IShape
    {
        private IShape m_shape;
        private SpellShapeEnum m_shapeType;

        public Zone(SpellShapeEnum shape, uint radius)
        {
            ShapeType = shape;
            Radius = radius;
        }

        public SpellShapeEnum ShapeType
        {
            get { return m_shapeType; }
            set
            {
                m_shapeType = value;
                InitializeShape();
            }
        }

        #region IShape Members

        public uint Surface
        {
            get { return m_shape.Surface; }
        }

        public uint MinRadius
        {
            get { return m_shape.MinRadius; }
            set { m_shape.MinRadius = value; }
        }

        public DirectionsEnum Direction
        {
            get { return m_shape.Direction; }
            set { m_shape.Direction = value; }
        }

        public uint Radius
        {
            get { return m_shape.Radius; }
            set { m_shape.Radius = value; }
        }

        public Cell[] GetCells(Cell centerCell, Map map)
        {
            return m_shape.GetCells(centerCell, map);
        }

        #endregion

        private void InitializeShape()
        {
            switch (ShapeType)
            {
                case SpellShapeEnum.X:
                    m_shape = new Cross(0, Radius);
                    break;
                case SpellShapeEnum.L:
                    m_shape = new Line(Radius);
                    break;
                case SpellShapeEnum.T:
                    m_shape = new Cross(0, Radius)
                                  {
                                      OnlyPerpendicular = true
                                  };
                    break;
                case SpellShapeEnum.D:
                    m_shape = new Cross(0, Radius);
                    break;
                case SpellShapeEnum.C:
                    m_shape = new Lozenge(0, Radius);
                    break;
                case SpellShapeEnum.I:
                    m_shape = new Lozenge(Radius, 63);
                    break;
                case SpellShapeEnum.O:
                    m_shape = new Cross(1, Radius);
                    break;
                case SpellShapeEnum.Q:
                    m_shape = new Cross(1, Radius);
                    break;
                case SpellShapeEnum.V:
                    m_shape = new Cone(0, Radius);
                    break;
                case SpellShapeEnum.W:
                    m_shape = new Square(0, Radius)
                                  {
                                      DiagonalFree = true
                                  };
                    break;
                case SpellShapeEnum.plus:
                    m_shape = new Cross(0, Radius)
                                  {
                                      Diagonal = true
                                  };
                    break;
                case SpellShapeEnum.sharp:
                    m_shape = new Cross(1, Radius)
                                  {
                                      Diagonal = true
                                  };
                    break;
                case SpellShapeEnum.star:
                    m_shape = new Cross(0, Radius)
                                  {
                                      AllDirections = true
                                  };
                    break;
                case SpellShapeEnum.slash:
                    m_shape = new Line(Radius);
                    break;
                case SpellShapeEnum.U:
                    m_shape = new HalfLozenge(0, Radius);
                    break;
                case SpellShapeEnum.A:
                    m_shape = new Lozenge(0, 63);
                    break;
                default:
                    m_shape = new Cross(0, 0);
                    break;
            }
        }
    }
}