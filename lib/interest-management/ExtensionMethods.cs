﻿using System.Collections.Generic;

namespace InterestManagement
{
    public static class ExtensionMethods
    {
        public static IEnumerable<IRegion<TSceneObject>> GetRegions<TSceneObject>(
            this IMatrixRegion<TSceneObject> @this,
            ITransform transform)
            where TSceneObject : ISceneObject
        {
            var vertices = Rectangle.GetVertices(
                transform.Position,
                transform.Size);

            return @this.GetRegions(vertices);
        }
    }
}