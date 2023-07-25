﻿using Microsoft.EntityFrameworkCore;
using NationalGeographicMessager.Domain.GeolocationAggregated;
using NationalGeographicMessager.Domain.OcurrencesAggregated;
using NationalGeographicWorker.Infrastructure.Persistence;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using Point = NationalGeographicMessager.Domain.GeolocationAggregated.Point;

namespace NationalGeographicMessager.Infrastructure.Repositories
{
    internal class EFCoreOccurrenceRepository : IOccurrenceRepository
    {
        private readonly NationalGeographicDbContext _dbCcontext;
        private readonly IGeoLocationCalculator _geoLocationCalculator;

        public EFCoreOccurrenceRepository(
            NationalGeographicDbContext dbCcontext, 
            IGeoLocationCalculator geoLocationCalculator
            )
        {
            _dbCcontext = dbCcontext;
            _geoLocationCalculator = geoLocationCalculator;
        }

        public async Task<IEnumerable<OccurrenceEventMessage>> GetAsync(Point occurrenceLocation)
        {
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
            var radiusInDegrees = _geoLocationCalculator.KilometerRadiusToDegreesRadius(occurrenceLocation.Radius);

            var targetPoint = geometryFactory.CreatePoint(new Coordinate(occurrenceLocation.Longitude, occurrenceLocation.Latitude));
            
            return await _dbCcontext.Occurrences
                              .Where(occurrence => occurrence.Geolocation.Distance(targetPoint) <= radiusInDegrees)
                              .Select(occurrence => new OccurrenceEventMessage {
                                  OccurrenceId =  occurrence.OccurrenceId,
                                  Location = occurrence.Location,
                                  OccurrenceName = occurrence.OccurrenceName,
                                  StatusName = occurrence.StatusName,
                                  OccurrenceTypeName = occurrence.OccurrenceTypeName,
                                  OccurrenceUrgencyLevelName = occurrence.OccurrenceUrgencyLevelName,
                                  UserId = occurrence.UserId,
                                  UserName = occurrence.UserName,
                                  Active = occurrence.Active,
                                  Latitude = occurrence.Geolocation.Y,
                                  Longitude = occurrence.Geolocation.X
                              })
                              .ToListAsync();
        }
    }
}
