public class Startup{
  public IConfiguration Configuration{ get; }

  public Startup(IConfiguration configuration){
    Configuration = configuration;
  }

  public void ConfigureServices(IServiceCollection services){
    services.AddControllers();
    services.AddSingleton<IConfiguration>(Configuration);
}

  public void Configure(IApplicationBuilder app, IWebHostEnvironment env){
    if (env.IsDevelopment()){
      app.UseDeveloperExceptionPage();
     // app.UseSwagger();
     // app.UseSwaggerUI();
      
    }
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.UseRouting();
    app.UseEndpoints(endpoints =>{endpoints.MapControllers();});
  }
}