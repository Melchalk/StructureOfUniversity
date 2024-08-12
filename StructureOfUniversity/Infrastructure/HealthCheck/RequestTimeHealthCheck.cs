using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Diagnostics;

namespace StructureOfUniversity.Infrastructure.HealthCheck;

public class RequestTimeHealthCheck : IHealthCheck
{
    private readonly HttpClient _httpClient;

    private readonly int _standardTime = 500;

    public RequestTimeHealthCheck(HttpClient client)
    {
        _httpClient = client;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();

        await _httpClient.GetAsync("http://localhost:5084/api/student/get/all");

        stopwatch.Stop();

        var responseTime = stopwatch.ElapsedMilliseconds;

        if (responseTime > _standardTime * 10)
        {
            return HealthCheckResult.Unhealthy("A restart is required");
        }
        else if (responseTime > _standardTime)
        {
            return HealthCheckResult.Unhealthy("Decline of productivity");
        }

        return HealthCheckResult.Healthy("Ok");
    }
}
