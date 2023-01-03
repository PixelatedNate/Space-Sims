public static class PersonVoice
{

    public static float GetPitch(PersonInfo personInfo)
    {
        float pitch = 1f;
        switch (personInfo.Race)
        {
            case Race.Alien:
                {
                    if (personInfo.Gender == Gender.Male)
                    {
                        pitch = 1.9f;
                    }
                    else if (personInfo.Gender == Gender.Female)
                    {
                        pitch = 2.1f;
                    }
                    return pitch;
                }
            case Race.Aquatic:
                {
                    if (personInfo.Gender == Gender.Male)
                    {
                        pitch = 1.2f;
                    }
                    else if (personInfo.Gender == Gender.Female)
                    {
                        pitch = 1.2f;
                    }
                    return pitch;
                }

            case Race.Avian:
                {
                    if (personInfo.Gender == Gender.Male)
                    {
                        pitch = 0.6f;
                    }
                    if (personInfo.Gender == Gender.Female)
                    {
                        pitch = 1.25f;
                    }
                    return pitch;
                }

            case Race.Bovine:
                {
                    if (personInfo.Gender == Gender.Male)
                    {
                        pitch = 1.2f;
                    }
                    if (personInfo.Gender == Gender.Female)
                    {
                        pitch = 1.3f;
                    }
                    return pitch;
                }

            case Race.Canine:
                {
                    if (personInfo.Gender == Gender.Male)
                    {
                        pitch = 0.3f;
                    }
                    if (personInfo.Gender == Gender.Female)
                    {
                        pitch = 0.4f;
                    }
                    return pitch;
                }


            case Race.Feline:
                {
                    if (personInfo.Gender == Gender.Male)
                    {
                        pitch = 1.8f;
                    }
                    if (personInfo.Gender == Gender.Female)
                    {
                        pitch = 2f;
                    }
                    return pitch;
                }

            case Race.Human:
                {
                    if (personInfo.Gender == Gender.Male)
                    {
                        pitch = 1f;
                    }
                    if (personInfo.Gender == Gender.Female)
                    {
                        pitch = 1.3f;
                    }
                    return pitch;
                }

            case Race.Insectoid:
                {
                    if (personInfo.Gender == Gender.Male)
                    {
                        pitch = 0.5f;
                    }
                    if (personInfo.Gender == Gender.Female)
                    {
                        pitch = 0.7f;
                    }
                    return pitch;

                }

            case Race.Plant:
                {
                    if (personInfo.Gender == Gender.Male)
                    {
                        pitch = 2.5f;
                    }
                    if (personInfo.Gender == Gender.Female)
                    {
                        pitch = 2.5f;
                    }
                    return pitch;
                }

            case Race.Reptile:
                {
                    if (personInfo.Gender == Gender.Male)
                    {
                        pitch = 1.5f;
                    }
                    if (personInfo.Gender == Gender.Female)
                    {
                        pitch = 1.1f;
                    }
                    return pitch;
                }

            case Race.Robotic:
                {
                    if (personInfo.Gender == Gender.Male)
                    {
                        pitch = 0.4f;
                    }
                    if (personInfo.Gender == Gender.Female)
                    {
                        pitch = 0.4f;
                    }
                    return pitch;
                }
        }
        return 1f;
    }




}
