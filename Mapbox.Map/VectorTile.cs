﻿//-----------------------------------------------------------------------
// <copyright file="VectorTile.cs" company="Mapbox">
//     Copyright (c) 2016 Mapbox. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Mapbox.Map
{
    using System;
    using System.Collections.ObjectModel;

    using Mapbox.Utils;
    using Mapbox.Vector.Tile;

    // using Mapbox.VectorTile;
    // using Mapbox.VectorTile.ExtensionMethods;

    /// <summary>
    ///    A decoded vector tile, as specified by the
    ///    <see href="https://www.mapbox.com/vector-tiles/specification/">
    ///    Mapbox Vector Tile specification</see>. 
    ///    See available layers and features <see href="https://www.mapbox.com/vector-tiles/mapbox-streets-v7/">here</see>.
    ///    The tile might be incomplete if the network request and parsing are still pending.
    /// </summary>
    ///  <example>
    /// Making a VectorTile request:
    /// <code>
    /// var parameters = new Tile.Parameters();
    /// parameters.Fs = MapboxAccess.Instance;
    /// parameters.Id = new CanonicalTileId(_zoom, _tileCoorindateX, _tileCoordinateY);
    /// parameters.MapId = "mapbox.mapbox-streets-v7";
    /// var vectorTile = new VectorTile();
    /// 
    /// // Make the request.
    /// vectorTile.Initialize(parameters, (Action)(() =>
    /// {
    /// 	if (!string.IsNullOrEmpty(vectorTile.Error))
    /// 	{
    ///			// Handle the error.
    ///		}
    /// 
    /// 	// Consume the <see cref="Data"/>.
    ///	}));
    /// </code>
    /// </example>
    public sealed class VectorTile : Tile, IDisposable
    {
        // FIXME: Namespace here is very confusing and conflicts (sematically)
        // with his class. Something has to be renamed here.
        private VectorTile data;

        private bool isDisposed = false;

        /// <summary> Gets the vector decoded using Mapbox.VectorTile library. </summary>
        /// <value> The GeoJson data. </value>
        public VectorTile Data
        {
            get
            {
                return this.data;
            }
        }

        /// <summary>
        /// <para>Gets the vector in a GeoJson format.</para>
        /// <para>
        /// This method should be avoided as it fully decodes the whole tile and might pose performance and memory bottle necks.
        /// </para>
        /// </summary>
        /// <value> The GeoJson data. </value>
        /// <example>
        /// Inspect the GeoJson.
        /// <code>
        /// var json = VectorTile.GeoJson;
        /// Console.Write("GeoJson: " + json);
        /// </code>
        /// </example>
        public string GeoJson
        {
            get
            {
                throw new NotImplementedException();

                // return this.data.ToGeoJson((ulong)Id.Z, (ulong)Id.X, (ulong)Id.Y, 0);
            }
        }

        // TODO: uncomment if 'VectorTile' class changes from 'sealed'
        // protected override void Dispose(bool disposeManagedResources)
        // ~VectorTile()
        // {
        // Dispose(false);
        // }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // TODO: change signature if 'VectorTile' class changes from 'sealed'
        // protected override void Dispose(bool disposeManagedResources)
        public void Dispose(bool disposeManagedResources)
        {
            if (!isDisposed)
            {
                if (disposeManagedResources)
                {
                    // TODO implement IDisposable with Mapbox.VectorTile.VectorTile
                    if (null != data)
                    {
                        data = null;
                    }
                }
            }
        }

        // FIXME: Why don't these work?
        /// <summary>
        /// Decodes the requested layer.
        /// </summary>
        /// <param name="layerName">Name of the layer to decode.</param>
        /// <returns>Decoded VectorTileLayer or 'null' if an invalid layer name was specified.</returns>
        /// <example>
        /// Inspect a layer of the vector tile.
        /// <code>
        /// var countryLabelLayer = vectorTile.GetLayer("country_label");
        /// var count = countryLabelLayer.Keys.Count;
        /// for (int i = 0; i &lt; count; i++)
        /// {
        /// 	Console.Write(string.Format("{0}:{1}", countryLabelLayer.Keys[i], countryLabelLayer.Values[i]));
        /// }
        /// </code>
        /// </example>
        public VectorTileLayer GetLayer(string layerName)
        {
            return this.data.GetLayer(layerName);
        }

        /// <summary>
        /// Gets all availble layer names.
        /// See available layers and features <see href="https://www.mapbox.com/vector-tiles/mapbox-streets-v7/">here</see>.
        /// </summary>
        /// <returns>Collection of availble layers.</returns>
        /// <example>
        /// Inspect the LayerNames.
        /// <code>
        /// var layerNames = vectorTile.LayerNames();
        /// foreach (var layer in layerNames)
        /// {
        /// 	Console.Write("Layer: " + layer);
        /// }
        /// </code>
        /// </example>
        public ReadOnlyCollection<string> LayerNames()
        {
            return this.data.LayerNames();
        }

        internal override TileResource MakeTileResource(string mapId)
        {
            return TileResource.MakeVector(Id, mapId);
        }

        internal override bool ParseTileData(byte[] data)
        {
            try
            {
                var decompressed = Compression.Decompress(data);

                this.data = new VectorTile();

                return true;
            }
            catch (Exception ex)
            {
                AddException(ex);
                return false;
            }
        }
    }
}