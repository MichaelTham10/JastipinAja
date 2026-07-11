using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace JastipinAja.BuildingBlocks.Generic
{
    public sealed record RunningNumberRequest(
    string SequenceName,      // mis. "ordering.OrderNoSeq"
    string Prefix,            // mis. "ORD"
    bool IncludeYear = true,
    int PadWidth = 5);


    public sealed class RunningNumberGenerator<TContext> where TContext : DbContext
    {
        private readonly TContext _db;
        public RunningNumberGenerator(TContext db) => _db = db;

        public async Task<string> NextAsync(RunningNumberRequest r, CancellationToken ct)
        {
            if (!Regex.IsMatch(r.SequenceName, @"^[A-Za-z0-9_.""]+$"))
                throw new ArgumentException($"Nama sequence tidak valid: {r.SequenceName}");

            var seq = await _db.Database
                .SqlQueryRaw<long>($"SELECT nextval('{r.SequenceName}') AS \"Value\"")
                .FirstAsync(ct);

            var year = r.IncludeYear ? $"-{DateTime.UtcNow:yyyy}" : "";
            return $"{r.Prefix}{year}-{seq.ToString().PadLeft(r.PadWidth, '0')}";
        }
    }
}
