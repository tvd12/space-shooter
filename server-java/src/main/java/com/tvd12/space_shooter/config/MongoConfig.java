package com.tvd12.space_shooter.config;

import com.mongodb.MongoClient;
import com.tvd12.ezydata.database.EzyDatabaseContext;
import com.tvd12.ezydata.mongodb.EzyMongoDatabaseContextBuilder;
import com.tvd12.ezydata.mongodb.loader.EzySimpleMongoClientLoader;
import com.tvd12.ezyfox.annotation.EzyProperty;
import com.tvd12.ezyfox.bean.EzyBeanConfig;
import com.tvd12.ezyfox.bean.EzySingletonFactory;
import com.tvd12.ezyfox.bean.EzySingletonFactoryAware;
import com.tvd12.ezyfox.bean.annotation.EzyConfigurationBefore;
import com.tvd12.ezyfox.util.EzyLoggable;
import com.tvd12.ezyfox.util.EzyPropertiesAware;
import lombok.Setter;

import java.util.Map;
import java.util.Properties;

@Setter
@EzyConfigurationBefore
public class MongoConfig
        extends EzyLoggable
        implements EzyPropertiesAware, EzySingletonFactoryAware, EzyBeanConfig {

    @EzyProperty("database.mongo.database")
    private String databaseName;

    private Properties properties;
    private EzySingletonFactory singletonFactory;

    @Override
    public void config() {
        EzyDatabaseContext databaseContext = newMongodbDatabaseContext();
        Map<String, Object> repos = databaseContext.getRepositoriesByName();
        for(String repoName : repos.keySet())
            singletonFactory.addSingleton(repoName, repos.get(repoName));
    }

    private EzyDatabaseContext newMongodbDatabaseContext() {
        return new EzyMongoDatabaseContextBuilder()
                .properties(properties)
                .mongoClient(newMongoClient())
                .databaseName(databaseName)
                .scan("com.tvd12.space_shooter.entity")
                .scan("com.tvd12.space_shooter.model")
                .scan("com.tvd12.space_shooter.repo")
                .build();
    }

    private MongoClient newMongoClient() {
        return EzySimpleMongoClientLoader.load(properties);
    }
}
