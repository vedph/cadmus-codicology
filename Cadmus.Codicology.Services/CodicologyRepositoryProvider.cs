﻿using System;
using System.Reflection;
using Cadmus.Core;
using Cadmus.Core.Config;
using Cadmus.Core.Storage;
using Cadmus.Mongo;
using Cadmus.Codicology.Parts;
using Fusi.Tools.Configuration;

namespace Cadmus.Codicology.Services;

/// <summary>
/// Cadmus Codicology repository provider.
/// </summary>
/// <seealso cref="IRepositoryProvider" />
[Tag("repository-provider.codicology")]
public sealed class CodicologyRepositoryProvider : IRepositoryProvider
{
    private readonly IPartTypeProvider _partTypeProvider;

    /// <summary>
    /// The connection string.
    /// </summary>
    public string ConnectionString { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CodicologyRepositoryProvider"/>
    /// class.
    /// </summary>
    /// <exception cref="ArgumentNullException">configuration</exception>
    public CodicologyRepositoryProvider()
    {
        ConnectionString = "";
        TagAttributeToTypeMap map = new();
        map.Add(new[]
        {
            // Cadmus.Codicology.Parts
            typeof(CodBindingsPart).GetTypeInfo().Assembly,
        });

        _partTypeProvider = new StandardPartTypeProvider(map);
    }

    /// <summary>
    /// Gets the part type provider.
    /// </summary>
    /// <returns>part type provider</returns>
    public IPartTypeProvider GetPartTypeProvider()
    {
        return _partTypeProvider;
    }

    /// <summary>
    /// Creates a Cadmus repository.
    /// </summary>
    /// <returns>repository</returns>
    public ICadmusRepository CreateRepository()
    {
        // create the repository (no need to use container here)
        MongoCadmusRepository repository =
            new(
                _partTypeProvider,
                new StandardItemSortKeyBuilder());

        repository.Configure(new MongoCadmusRepositoryOptions
        {
            ConnectionString = ConnectionString ??
                throw new InvalidOperationException(
                "No connection string set for IRepositoryProvider implementation")
        });

        return repository;
    }
}
