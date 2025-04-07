DO $$ 
DECLARE
    default_user TEXT := 'defaultUser';
    default_password TEXT := 'defaultPassword';
BEGIN
    EXECUTE format('CREATE USER %I WITH PASSWORD %L', default_user, default_password);
    EXECUTE format('GRANT ALL PRIVILEGES ON DATABASE %I TO %I', current_database(), default_user);

    EXECUTE format('ALTER ROLE %I SET search_path TO public', default_user);
    EXECUTE format('GRANT USAGE, CREATE ON SCHEMA public TO %I', default_user);
    EXECUTE format('GRANT ALL PRIVILEGES ON ALL TABLES IN SCHEMA public TO %I', default_user);
    EXECUTE format('GRANT ALL PRIVILEGES ON ALL SEQUENCES IN SCHEMA public TO %I', default_user);

    EXECUTE format('ALTER DEFAULT PRIVILEGES FOR ROLE %I IN SCHEMA public GRANT ALL ON TABLES TO %I', default_user, default_user);
    EXECUTE format('ALTER DEFAULT PRIVILEGES FOR ROLE %I IN SCHEMA public GRANT ALL ON SEQUENCES TO %I', default_user, default_user);
END $$;